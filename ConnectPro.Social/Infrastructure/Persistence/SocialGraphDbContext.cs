using Microsoft.EntityFrameworkCore;
using Social.Domain.Aggregates.Follow.Entities;
using Social.Domain.Aggregates.Like.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Social.Infrastructure.Persistence
{
    public sealed class SocialGraphDbContext(DbContextOptions<SocialGraphDbContext> options)
    : DbContext(options)
    {
        public DbSet<Follow> Follows => Set<Follow>();
        public DbSet<Like> Likes => Set<Like>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Social");
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
