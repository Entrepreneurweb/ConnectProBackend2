using ConnectPro.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Aggregates.Portfolio.ValueObject;
using Portfolio.Domain.Aggregates.Service.Entities;
using Portfolio.Domain.Aggregates.Service.ValueObject;
using Portfolio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Infrastructure.Persistence.Configurations
{

    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("Services");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                .HasConversion(
                    id => id.Value,
                    value => new Id<Service>(value))
                .ValueGeneratedNever();

            builder.Property<Guid>("_portfolioId")
                .HasColumnName("PortfolioId")
                .IsRequired();

            builder.Property<string>("_title")
                .HasColumnName("Title")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property<string>("_description")
                .HasColumnName("Description")
                .HasMaxLength(2000)
                .IsRequired();

            builder.Property<ServiceStatus>("_status")
                .HasColumnName("Status")
                .HasConversion<string>()
                .IsRequired();

            // Ignorer les propriétés publiques IReadOnlyList
            builder.Ignore(s => s.Tags);
            builder.Ignore(s => s.ImageUrls);
            builder.Ignore(s => s.Faqs);
            builder.Ignore(s => s.Awards);
            builder.Ignore(s => s.CoverImageUrl);
            builder.Ignore(s => s.Pricing);

            // CoverImageUrl → aplati dans Services
            builder.OwnsOne<Url>("_coverImageUrl", u =>
            {
                u.Property(x => x.Value).HasColumnName("CoverImageUrl").HasMaxLength(500);
            });

            // Pricing → aplati dans Services
            //builder.OwnsOne<Pricing>("_pricing", p =>
            //{
            //    p.Property(x => x.Amount).HasColumnName("Pricing_Amount").HasColumnType("decimal(18,2)");
            //    p.Property(x => x.Currency).HasColumnName("Pricing_Currency").HasMaxLength(3);
            //    p.Property(x => x.Type).HasColumnName("Pricing_Type").HasConversion<string>().HasMaxLength(20);
            //});
            builder.OwnsOne<Pricing>("_pricing", p =>
            {
                p.Property( x => x.Amount).HasColumnName("Pricing_Amount").HasColumnType("decimal(18,2)");
                p.Property(x => x.currency).HasColumnName("Pricing_Currency").HasConversion<string>().HasMaxLength(5);
                p.Property(x => x.Type).HasColumnName("Pricing_Type").HasConversion<string>().HasMaxLength(20);
            });

            // Tags → table séparée (Entité)
            builder.HasMany<Tag>("_tags")
                .WithOne()
                .HasForeignKey(t => t.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            // ImageUrls → table séparée (Entité)
            builder.HasMany<ImageUrl>("_imageUrls")
                .WithOne()
                .HasForeignKey(i => i.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            // FAQs → table séparée (Entité)
            builder.HasMany<FAQ>("_faqs")
                .WithOne()
                .HasForeignKey(f => f.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Awards → table séparée (Entité)
            builder.HasMany<Award>("_awards")
                .WithOne()
                .HasForeignKey(a => a.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex("_portfolioId")
                .HasDatabaseName("IX_Services_PortfolioId");
        }
    }
}
