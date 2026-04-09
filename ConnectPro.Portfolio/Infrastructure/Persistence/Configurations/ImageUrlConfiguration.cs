using ConnectPro.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Aggregates.Portfolio.ValueObject;
using Portfolio.Domain.Aggregates.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Infrastructure.Persistence.Configurations
{
    public class ImageUrlConfiguration : IEntityTypeConfiguration<ImageUrl>
    {
        public void Configure(EntityTypeBuilder<ImageUrl> builder)
        {
            builder.ToTable("Service_ImageUrls");

            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id)
                .HasConversion(
                    id => id.Value,
                    value => new Id<ImageUrl>(value))
                .ValueGeneratedNever();
            builder.Property(i => i.ServiceId).IsRequired();

            builder.OwnsOne<Url>("_value", u =>
            {
                u.Property(x => x.Value).HasColumnName("Url").HasMaxLength(500).IsRequired();
            });

            builder.HasIndex(i => i.ServiceId)
                .HasDatabaseName("IX_ImageUrls_ServiceId");
        }
    }
}
