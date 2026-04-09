using ConnectPro.Identity.Domain.Aggregates.ValueObjects;
using ConnectPro.SharedKernel;
using Identity.Domain.Aggregates.User.Entities;
using Identity.Domain.Aggregates.ValueObjects;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectPro.Identity.Infrastructure.Persistence
{
    internal sealed class ApplicationUser : IdentityUser<Guid>
    {
       // public string Role { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public Otp? pendingOtp;

        public string ProfilePictureUrl;

        // Exposé en internal pour que UserRepository puisse vérifier le hash
        internal string GetPasswordHash() => PasswordHash ?? string.Empty;

        public User ToDomain() =>

            User.Reconstitute(

               // new UserId(Id),
               new Id<User>(Id),

                new Email(Email!),
                new PasswordHash(PasswordHash),               
                IsActive,
                pendingOtp,
                ProfilePictureUrl
            );
        
        public static ApplicationUser FromDomain(User user) =>
            new()
            {
                Id = user.Id.Value,
                UserName = user.GetEmail().Value,
                Email = user.GetEmail().Value,
                NormalizedEmail = user.GetEmail().Value.ToUpperInvariant(),
                NormalizedUserName = user.GetEmail().Value.ToUpperInvariant(),
               // Role = user.Role.ToString(),
                CreatedAt = user.CreatedAtUtc.DateTime,
                IsActive = user.IsActive(),
                pendingOtp = user.GetPendingOtp(),
                ProfilePictureUrl = user.GetProfilePictureUrl(),

                // IsActive = user.IsActive,
                PasswordHash = user.GetPasswordHash().Value ,
            }    ;

        public void SyncFromDomain(User user)
        {
            UserName = user.GetEmail().Value;
            Email = user.GetEmail().Value;
            NormalizedEmail = user.GetEmail().Value.ToUpperInvariant();
            NormalizedUserName = user.GetEmail().Value.ToUpperInvariant();
           // Role = user.Role.ToString();
            IsActive = user.IsActive();
            PasswordHash = user.GetPasswordHash().Value ;
            pendingOtp = user.GetPendingOtp();
            ProfilePictureUrl = user.GetProfilePictureUrl();
        }
    }
}
