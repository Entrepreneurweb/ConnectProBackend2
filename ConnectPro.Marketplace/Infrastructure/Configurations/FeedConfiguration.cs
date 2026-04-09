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
    public sealed class FeedConfiguration : IEntityTypeConfiguration<Feed>
    {
        public void Configure(EntityTypeBuilder<Feed> builder)
        {
            builder.ToTable("Feeds");
            
            builder.Property( f => f.Id)
                .HasConversion( id => id.Value , value => new Id<Feed>(value)) ;

            builder.HasKey(f => f.Id);

            builder.Property(f => f.ClientId)
                .IsRequired();

            builder.HasMany<FeedItem>(f => f.Items)
                .WithOne()
                .HasForeignKey("FeedId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(f => f.Items)
                .HasField("_items")
                .AutoInclude();
        }
    }
}
