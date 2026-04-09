using ConnectPro.SharedKernel;
using Marketplace.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain.Aggregates.Feed.Entities
{
    public sealed class Feed : AggregateRoot<Feed>
    {
        private readonly List<FeedItem> _items = new();

        public Guid ClientId { get; private set; }

        public IReadOnlyCollection<FeedItem> Items => _items.AsReadOnly();

        private Feed() { }

        public static Feed Create(Guid clientId)
        {
            if (clientId == Guid.Empty)
                throw new ArgumentException("ClientId is required.", nameof(clientId));

            return new Feed
            {
                Id = clientId,
                ClientId = clientId
            };
        }

        public void Recompute(IEnumerable<(Guid RefId, string RefType, double Score)> items)
        {
            _items.Clear();

            foreach (var item in items)
            {
                var refType = item.RefType switch
                {
                    "JobPost" => FeedItemType.JobPost,
                    "Service" => FeedItemType.Service,
                    _ => throw new ArgumentException($"Unknown FeedItemType: {item.RefType}")
                };

                _items.Add(FeedItem.Create(item.RefId, refType, item.Score));
            }
        }
    }
}
