using ConnectPro.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Aggregates.Portfolio.Entities;
using Portfolio.Domain.Aggregates.Portfolio.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Infrastructure.Persistence.Configurations
{
    public class ExperienceConfiguration : IEntityTypeConfiguration<Experience>
    {
        public void Configure(EntityTypeBuilder<Experience> builder)
        {
            builder.ToTable("Experiences");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasConversion(
                    id => id.Value,
                    value => new Id<Experience>(value))
                .ValueGeneratedNever();
            builder.Property(e => e.PortfolioId).IsRequired();

            builder.Property<string>("_company")
                .HasColumnName("Company")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property<string>("_role")
                .HasColumnName("Role")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property<string?>("_description")
                .HasColumnName("Description")
                .HasMaxLength(1000);

            builder.OwnsOne<Domain.Aggregates.Portfolio.ValueObject.DateRange>("_period", dr =>
            {
                dr.Property(x => x.Start).HasColumnName("StartDate").IsRequired();
                dr.Property(x => x.End).HasColumnName("EndDate");
            });

            builder.HasIndex(e => e.PortfolioId)
                .HasDatabaseName("IX_Experiences_PortfolioId");
        }
    }
}
