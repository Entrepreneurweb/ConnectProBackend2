using ConnectPro.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Aggregates.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Infrastructure.Persistence.Configurations
{
    public class AwardConfiguration : IEntityTypeConfiguration<Award>
    {
        public void Configure(EntityTypeBuilder<Award> builder)
        {
            builder.ToTable("Service_Awards");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).HasConversion(
                    id => id.Value,
                    value => new Id<Award>(value))

                .ValueGeneratedNever();
            builder.Property(a => a.ServiceId).IsRequired();

            builder.Property<string>("_title")
                .HasColumnName("Title")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property<string>("_issuedBy")
                .HasColumnName("IssuedBy")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property<DateOnly>("_issuedAt")
                .HasColumnName("IssuedAt")
                .IsRequired();

            builder.Property<string?>("_description")
                .HasColumnName("Description")
                .HasMaxLength(1000);

            builder.HasIndex(a => a.ServiceId)
                .HasDatabaseName("IX_Awards_ServiceId");
        }
    }
}
