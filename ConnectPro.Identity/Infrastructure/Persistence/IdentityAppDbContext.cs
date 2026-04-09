using ConnectPro.SharedKernel.Events;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectPro.Identity.Infrastructure.Persistence
{
    internal sealed class IdentityAppDbContext
     : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        private readonly IPublisher _publisher;

        public IdentityAppDbContext(DbContextOptions<IdentityAppDbContext> options, IPublisher publisher)
            : base(options)
        {
            _publisher = publisher;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(IdentityAppDbContext).Assembly);
            builder.HasDefaultSchema("Identity");

            // Renommer les tables ASP.NET Identity
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole<Guid>>().ToTable("Roles");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
        }

        public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            var result = await base.SaveChangesAsync(ct);
            await DispatchDomainEventsAsync(ct);
            return result;
        }

        // Les domain events sont dispatchés APRÈS la persistance
        private async Task DispatchDomainEventsAsync(CancellationToken ct)
        {
            var domainEvents = ChangeTracker
                .Entries<object>()
                .Select(e => e.Entity)
                .OfType<IHasDomainEvents>()
                .SelectMany(e =>
                {
                    var events = e.DomainEvents.ToList();
                    e.ClearDomainEvents();
                    return events;
                })
                .ToList();

            foreach (var domainEvent in domainEvents)
                await _publisher.Publish(domainEvent, ct);
        }
    }

    // Interface légère pour accéder aux domain events sans coupler au type concret
    public interface IHasDomainEvents
    {
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
        void ClearDomainEvents();
    }
}
