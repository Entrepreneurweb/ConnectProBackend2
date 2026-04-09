using Identity.Domain.Aggregates.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectPro.Identity.Infrastructure.Persistence
{
    internal sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {  /*
            builder.Property(u => u.Role)
                .IsRequired()
                .HasMaxLength(50);*/
           

            builder.Property(u => u.CreatedAt)
                .IsRequired();

            builder.Property(u => u.IsActive)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(254);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            //builder.OwnsOne(u => u.PendingOtp, otp =>
            //{
            //    // Conversion pour OtpCode (le plus souvent un record avec .Value)
            //    otp.Property(o => o.Code)
            //       .HasColumnName("OtpCode")
            //       .HasConversion(
            //           code => code.Value,                    // Vers la BDD (string ou Guid)
            //           value => OtpCode.Create(value)         // Depuis la BDD → reconstruit OtpCode
            //       )
            //       .IsRequired();

            //    // ExpiresAt est déjà un DateTime → pas besoin de conversion
            //    otp.Property(o => o.ExpiresAt)
            //       .HasColumnName("OtpExpiresAt")
            //       .IsRequired();

            //    // IsUsed est un bool → pas besoin de conversion
            //    otp.Property(o => o.IsUsed)
            //       .HasColumnName("OtpIsUsed")
            //       .IsRequired()
            //       .HasDefaultValue(false);

            //    otp.WithOwner();
            //});


            builder.OwnsOne(u => u.pendingOtp, otp =>
            {
                otp.Property(o => o.Code)
                   .HasColumnName("OtpCode")
                     .HasConversion(
                          code => code.Value,                     
                          value => OtpCode.Create(value)          
                     )
                   .IsRequired();

                otp.Property(o => o.ExpiresAt)
                   .HasColumnName("OtpExpiresAt")
                   .IsRequired();

                otp.Property(o => o.IsUsed)
                   .HasColumnName("OtpIsUsed")
                   .IsRequired()
                   .HasDefaultValue(false);

                // Optionnel : si tu veux que l'OTP soit supprimé quand il n'existe plus
                otp.WithOwner();
            });


            builder.Property(u => u.ProfilePictureUrl)
                    .HasMaxLength(2048);
        }
    }
}
