using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Social.Domain.Aggregates.Like.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Infrastructure.Persistence.Configurations
{
    public sealed class LikeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.ToTable("Likes");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id).ValueGeneratedNever();
            builder.Property(l => l.UserId).IsRequired();
            builder.Property(l => l.ServiceId).IsRequired();
            builder.Property(l => l.ServiceOwnerId).IsRequired();
            builder.Property(l => l.CreatedAt).IsRequired();

            builder.HasIndex(l => new { l.UserId, l.ServiceId }).IsUnique();

            builder.Ignore(l => l.DomainEvents);
        }
    }
}
