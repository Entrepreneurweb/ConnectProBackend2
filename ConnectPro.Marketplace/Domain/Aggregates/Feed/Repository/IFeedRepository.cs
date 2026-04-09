using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeedEntity = Marketplace.Domain.Aggregates.Feed.Entities.Feed;

namespace Marketplace.Domain.Aggregates.Feed.Repository
{
    public interface IFeedRepository
    {
        Task<FeedEntity?> GetByClientIdAsync(Guid clientId, CancellationToken cancellationToken = default);
        Task UpsertAsync(FeedEntity feed, CancellationToken cancellationToken = default);
    }
}
