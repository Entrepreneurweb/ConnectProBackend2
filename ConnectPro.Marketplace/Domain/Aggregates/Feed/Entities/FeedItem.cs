using ConnectPro.SharedKernel;
using Marketplace.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain.Aggregates.Feed.Entities
{
    public sealed class FeedItem : Entity<FeedItem>
    {
        public Guid RefId { get; private set; }
        public FeedItemType RefType { get; private set; }
        public double Score { get; private set; }

        private FeedItem() { }

        internal static FeedItem Create(Guid refId, FeedItemType refType, double score)
        {
            if (refId == Guid.Empty)
                throw new ArgumentException("RefId is required.", nameof(refId));

            return new FeedItem
            {
               // Id = Guid.NewGuid(),
                RefId = refId,
                RefType = refType,
                Score = score
            };
        }
    }
}
