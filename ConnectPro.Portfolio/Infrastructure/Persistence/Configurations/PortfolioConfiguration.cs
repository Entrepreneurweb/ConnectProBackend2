using ConnectPro.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Aggregates.Portfolio.Entities;
using Portfolio.Domain.Aggregates.Portfolio.ValueObject;
using Portfolio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioEntity = Portfolio.Domain.Aggregates.Portfolio.Entities.Portfolio;

namespace Portfolio.Infrastructure.Persistence.Configurations
{

    public class PortfolioConfiguration : IEntityTypeConfiguration<PortfolioEntity>
    {
        public void Configure(EntityTypeBuilder<PortfolioEntity> builder)
        {
            builder.ToTable("Portfolios");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .HasConversion(
                    id => id.Value,
                    value => new Id<PortfolioEntity>(value) )
                .ValueGeneratedNever();

            builder.Property<Guid>("_ownerId")
                .HasColumnName("OwnerId")
                .IsRequired();

            builder.Property<PortfolioType>("_type")
                .HasColumnName("Type")
                .HasConversion<string>()
                .IsRequired();

            builder.Property<PortfolioStatus>("_status")
                .HasColumnName("Status")
                .HasConversion<string>()
                .IsRequired();

            builder.Property<int>("_activeServicesCount")
                .HasColumnName("ActiveServicesCount")
                .IsRequired();

            // Ignorer les propriétés publiques IReadOnlyList
            builder.Ignore(p => p.SocialLinks);
            builder.Ignore(p => p.Experiences);
            builder.Ignore(p => p.Certifications);
            builder.Ignore(p => p.GeneralInfo);
            builder.Ignore(p => p.ContactInfo);
            builder.Ignore(p => p.LocationInfo);
            builder.Ignore(p => p.ProfessionalInfo);

            // GeneralInfo → table séparée
            builder.OwnsOne<GeneralInfo>("_generalInfo", gi =>
            {
                gi.Ignore(x => x.Id);
                gi.ToTable("Portfolio_GeneralInfos");
                gi.WithOwner().HasForeignKey("PortfolioId");
                gi.HasKey("PortfolioId");
                gi.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
                gi.Property(x => x.LastName).HasMaxLength(100).IsRequired();
                gi.Property(x => x.Bio).HasMaxLength(500);
                gi.OwnsOne<Url>(x => x.AvatarUrl, url =>
                {
                    url.Property(u => u.Value).HasColumnName("AvatarUrl").HasMaxLength(500);
                });
            });

            // ContactInfo → table séparée
            builder.OwnsOne<ContactInfo>("_contactInfo", ci =>
            {
                ci.Ignore(x => x.Id);
                ci.ToTable("Portfolio_ContactInfos");
                ci.WithOwner().HasForeignKey("PortfolioId");
                ci.HasKey("PortfolioId");
                ci.OwnsOne<Email>(x => x.Email, e =>
                {
                    e.Property(u => u.Value).HasColumnName("Email").HasMaxLength(200).IsRequired();
                });
                ci.OwnsOne<PhoneNumber>(x => x.Phone, p =>
                {
                    p.Property(u => u.Value).HasColumnName("Phone").HasMaxLength(50);
                });
                ci.OwnsOne<Url>(x => x.Website, w =>
                {
                    w.Property(u => u.Value).HasColumnName("Website").HasMaxLength(300);
                });
            });

            // LocationInfo → table séparée
            builder.OwnsOne<LocationInfo>("_locationInfo", li =>
            {
                li.Ignore(x => x.Id);
                li.ToTable("Portfolio_LocationInfos");
                li.WithOwner().HasForeignKey("PortfolioId");
                li.HasKey("PortfolioId");
                li.Property(x => x.Country).HasMaxLength(100).IsRequired();
                li.Property(x => x.City).HasMaxLength(100).IsRequired();
                li.Property(x => x.Timezone).HasMaxLength(100);
            });

            // ProfessionalInfo → table séparée
            builder.OwnsOne<ProfessionalInfo>("_professionalInfo", pi =>
            {
                pi.Ignore(x => x.Id);
                pi.ToTable("Portfolio_ProfessionalInfos");
                pi.WithOwner().HasForeignKey("PortfolioId");
                pi.HasKey("PortfolioId");
                pi.Property(x => x.Headline).HasMaxLength(200).IsRequired();
                pi.Ignore(x => x.Skills);
                pi.OwnsMany<Skill>("_skills", s =>
                {
                    s.ToTable("Portfolio_Skills");
                    s.WithOwner().HasForeignKey("PortfolioId");
                    s.Property(x => x.Name).HasMaxLength(100).IsRequired();
                    s.Property(x => x.Level).HasMaxLength(50).IsRequired();
                });
            });

            // SocialLinks → table séparée
            builder.OwnsMany<SocialLink>("_socialLinks", sl =>
            {   
                sl.ToTable("Portfolio_SocialLinks");
                sl.WithOwner().HasForeignKey("PortfolioId");
                sl.Property(x => x.Platform).HasMaxLength(100).IsRequired();
                sl.OwnsOne<Url>(x => x.Url, u =>
                {
                    u.Property(x => x.Value).HasColumnName("Url").HasMaxLength(500).IsRequired();
                });
                 
            });

            // Experiences → table séparée (Entité)
            builder.HasMany<Experience>("_experiences")
                .WithOne()
                .HasForeignKey(e => e.PortfolioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Certifications → table séparée (Entité)
            builder.HasMany<Certification>("_certifications")
                .WithOne()
                .HasForeignKey(c => c.PortfolioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
