using Marketplace.Application.Dtos;
using Marketplace.Domain.Aggregates.Feed.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Application.Feed.Queries.GetFeed
{
    public sealed class GetFeedHandler : IRequestHandler<GetFeedQuery, FeedDto?>
    {
        private readonly IFeedRepository _feedRepository;

        public GetFeedHandler(IFeedRepository feedRepository)
            => _feedRepository = feedRepository;

        public async Task<FeedDto?> Handle(GetFeedQuery query, CancellationToken cancellationToken)
        {
            var feed = await _feedRepository.GetByClientIdAsync(query.ClientId, cancellationToken);

            if (feed is null) return null;

            return new FeedDto(
                feed.ClientId,
                feed.Items.Select(i => new FeedItemDto(
                    i.RefId,
                    i.RefType.ToString(),
                    i.Score
                )).ToList()
            );
        }
    }
}
