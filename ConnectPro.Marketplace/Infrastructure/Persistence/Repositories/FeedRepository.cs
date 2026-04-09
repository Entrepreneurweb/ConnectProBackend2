using Marketplace.Domain.Aggregates.Feed.Entities;
using Marketplace.Domain.Aggregates.Feed.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Infrastructure.Persistence.Repositories
{
    public sealed class FeedRepository : IFeedRepository
    {
        private readonly MarketPlaceDbContext _context;

        public FeedRepository(MarketPlaceDbContext context)
            => _context = context;

        public async Task<Feed?> GetByClientIdAsync(Guid clientId, CancellationToken cancellationToken = default)
            => await _context.Feeds
                .FirstOrDefaultAsync(f => f.ClientId == clientId, cancellationToken);

        public async Task UpsertAsync(Feed feed, CancellationToken cancellationToken = default)
        {
            var exists = await _context.Feeds
                .AnyAsync(f => f.ClientId == feed.ClientId, cancellationToken);

            if (exists)
                _context.Feeds.Update(feed);
            else
                await _context.Feeds.AddAsync(feed, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
