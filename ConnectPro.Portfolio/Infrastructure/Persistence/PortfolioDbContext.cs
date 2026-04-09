using ConnectPro.Identity.Domain;
using ConnectPro.SharedKernel;
using ConnectPro.SharedKernel.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Aggregates.Portfolio.Entities;
using Portfolio.Domain.Aggregates.Review;
using Portfolio.Domain.Aggregates.Service.Entities;
using Portfolio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioEntity = Portfolio.Domain.Aggregates.Portfolio.Entities.Portfolio;

namespace Portfolio.Infrastructure.Persistence
{
    public class PortfolioDbContext : DbContext , IUnitOfWork
    {
        private readonly IPublisher _publisher;
        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options , IPublisher publisher) : base(options) 
        {
            _publisher = publisher;
        }

        public DbSet<PortfolioEntity> Portfolios => Set<PortfolioEntity>();
        public DbSet<Experience> Experiences => Set<Experience>();
        public DbSet<Certification> Certifications => Set<Certification>();
        public DbSet<Service> Services => Set<Service>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<ImageUrl> ImageUrls => Set<ImageUrl>();
        public DbSet<FAQ> Faqs => Set<FAQ>();
        public DbSet<Award> Awards => Set<Award>();
        public DbSet<Review> Reviews => Set<Review>();

        public override async Task<int> SaveChangesAsync(
           CancellationToken cancellationToken = default)
        {
             
            var domainEvents = ExtractDomainEvents();

            
            var result = await base.SaveChangesAsync(cancellationToken);

 
            await DispatchDomainEventsAsync(domainEvents, cancellationToken);

            return result;
        }

        private IReadOnlyList<IDomainEvent> ExtractDomainEvents()
        {
            var aggregates = ChangeTracker
                .Entries<IAggregateRoot>()
                .Where(entry => entry.Entity.DomainEvents.Any())
                .Select(entry => entry.Entity)
                .ToList();

            var events = aggregates
                .SelectMany(aggregate => aggregate.PopDomainEvents())
                .ToList();

            return events;
        }


        private async Task DispatchDomainEventsAsync(
            IReadOnlyList<IDomainEvent> domainEvents,
            CancellationToken cancellationToken)
        {
            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent, cancellationToken);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Portfolio");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PortfolioDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
