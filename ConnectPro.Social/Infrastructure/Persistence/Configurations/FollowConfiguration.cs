using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Social.Domain.Aggregates.Follow.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Infrastructure.Persistence.Configurations
{
    public sealed class FollowConfiguration : IEntityTypeConfiguration<Follow>
    {
        public void Configure(EntityTypeBuilder<Follow> builder)
        {
            builder.ToTable("Follows");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.Id).ValueGeneratedNever();
            builder.Property(f => f.FollowerId).IsRequired();
            builder.Property(f => f.PortfolioId).IsRequired();
            builder.Property(f => f.PortfolioOwnerId).IsRequired();
            builder.Property(f => f.CreatedAt).IsRequired();

            builder.HasIndex(f => new { f.FollowerId, f.PortfolioId }).IsUnique();

            builder.Ignore(f => f.DomainEvents);
        }
    }
}
