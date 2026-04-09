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
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Service_Tags");

            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
                .HasConversion(
                    id => id.Value,
                    value => new Id<Tag>(value))
                .ValueGeneratedNever();
            builder.Property(t => t.ServiceId).IsRequired();

            builder.Property<string>("_value")
                .HasColumnName("Value")
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(t => t.ServiceId)
                .HasDatabaseName("IX_Tags_ServiceId");
        }
    }
}
