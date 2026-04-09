using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Application.Feed.EventHandlers.FeedRecomputed
{
    /*
    public sealed class FeedRecomputedHandler : INotificationHandler<FeedRecomputed>
    {
        private readonly IFeedRepository _feedRepository;

        public FeedRecomputedHandler(IFeedRepository feedRepository)
            => _feedRepository = feedRepository;

        public async Task Handle(FeedRecomputed notification, CancellationToken cancellationToken)
        {
            var feed = await _feedRepository.GetByClientIdAsync(notification.ClientId, cancellationToken)
                       ?? Feed.Create(notification.ClientId);

            feed.Recompute(notification.Items.Select(i => (i.RefId, i.RefType, i.Score)));

            await _feedRepository.UpsertAsync(feed, cancellationToken);
        }
    }
    */

}
