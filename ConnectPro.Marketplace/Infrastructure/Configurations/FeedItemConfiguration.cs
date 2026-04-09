using ConnectPro.SharedKernel;
using Marketplace.Domain.Aggregates.Feed.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Infrastructure.Configurations
{

    public sealed class FeedItemConfiguration : IEntityTypeConfiguration<FeedItem>
    {
        public void Configure(EntityTypeBuilder<FeedItem> builder)
        {
            builder.ToTable("FeedItems");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.Id)
                .HasConversion(
                    id => id.Value,
                    value => new Id<FeedItem>(value) )
                .ValueGeneratedOnAdd();

            builder.Property(f => f.RefId)
                .IsRequired();

            builder.Property(f => f.RefType)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(f => f.Score)
                .IsRequired();
        }
    }
}
