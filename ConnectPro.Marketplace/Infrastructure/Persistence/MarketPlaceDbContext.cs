using Marketplace.Domain.Aggregates.Feed.Entities;
using Marketplace.Domain.Aggregates.JobPost.Entities;
using Marketplace.Domain.Aggregates.Service.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace Marketplace.Infrastructure.Persistence
{
    public sealed class MarketPlaceDbContext : DbContext
    {
        public MarketPlaceDbContext(DbContextOptions<MarketPlaceDbContext> options) : base(options) { }

        public DbSet<JobPost> JobPosts => Set<JobPost>();
        public DbSet<Feed> Feeds => Set<Feed>();
        public DbSet<ServiceProjection> ServiceProjections => Set<ServiceProjection>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("MarketPlace");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MarketPlaceDbContext).Assembly);
        }
    }

}
