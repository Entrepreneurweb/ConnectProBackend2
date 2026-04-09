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
    public class CertificationConfiguration : IEntityTypeConfiguration<Certification>
    {
        public void Configure(EntityTypeBuilder<Certification> builder)
        {
            builder.ToTable("Certifications");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasConversion(
                    id => id.Value,
                    value => new Id<Certification>(value))
                .ValueGeneratedNever();
            builder.Property(c => c.PortfolioId).IsRequired();

            builder.Property<string>("_name")
                .HasColumnName("Name")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property<string>("_issuingOrganization")
                .HasColumnName("IssuingOrganization")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property<DateOnly>("_issueDate")
                .HasColumnName("IssueDate")
                .IsRequired();

            builder.Property<DateOnly?>("_expiryDate")
                .HasColumnName("ExpiryDate");

            builder.OwnsOne<Url>("_credentialUrl", u =>
            {
                u.Property(x => x.Value).HasColumnName("CredentialUrl").HasMaxLength(500);
            });

            builder.HasIndex(c => c.PortfolioId)
                .HasDatabaseName("IX_Certifications_PortfolioId");
        }
    }

}
