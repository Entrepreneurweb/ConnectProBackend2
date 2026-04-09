using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Application.Queries.GetPortfolioFollowers
{
    public sealed record PortfolioFollowerDto(Guid FollowerId, DateTime FollowedAt);

    public sealed record GetPortfolioFollowersQuery(Guid PortfolioId)
        : IRequest<IReadOnlyList<PortfolioFollowerDto>>;

}
