using ConnectPro.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Aggregates.Review;
using Portfolio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Infrastructure.Persistence.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews");

            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id)
                .HasConversion(
                    id => id.Value,
                    value => new Id<Review>(value))
                .ValueGeneratedNever();

            builder.Property<Guid>("_serviceId")
                .HasColumnName("ServiceId")
                .IsRequired();

            builder.Property<Guid>("_reviewerId")
                .HasColumnName("ReviewerId")
                .IsRequired();

            builder.Property<ReviewStatus>("_status")
                .HasColumnName("Status")
                .HasConversion<string>()
                .IsRequired();

            builder.Property<string>("_comment")
                .HasColumnName("Comment")
                .HasMaxLength(1000)
                .IsRequired();

            builder.OwnsOne<Rating>("_rating", r =>
            {
                r.Property(x => x.Value)
                    .HasColumnName("Rating")
                    .IsRequired();
            });

            builder.HasIndex("_serviceId")
                .HasDatabaseName("IX_Reviews_ServiceId");

            builder.HasIndex("_serviceId", "_reviewerId")
                .IsUnique()
                .HasDatabaseName("IX_Reviews_ServiceId_ReviewerId");
        }
    }
}
