using Marketplace.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Application.Feed.Queries.GetFeed
{
    public sealed record GetFeedQuery(Guid ClientId) : IRequest<FeedDto?>;
}
