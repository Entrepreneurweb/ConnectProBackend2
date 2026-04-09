using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Application.Queries.GetUserFollowedPortfolios
{
    public sealed record FollowedPortfolioDto(Guid PortfolioId, DateTime FollowedAt);

    public sealed record GetUserFollowedPortfoliosQuery(Guid UserId)
      : IRequest<IReadOnlyList<FollowedPortfolioDto>>;

}
