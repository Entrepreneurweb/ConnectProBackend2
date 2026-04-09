using MediatR;
using Social.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Application.Queries.GetUserFollowedPortfolios
{
    public sealed class GetUserFollowedPortfoliosHandler(IFollowRepository followRepository)
    : IRequestHandler<GetUserFollowedPortfoliosQuery, IReadOnlyList<FollowedPortfolioDto>>
    {
        public async Task<IReadOnlyList<FollowedPortfolioDto>> Handle(
            GetUserFollowedPortfoliosQuery query, CancellationToken ct)
        {
            return await followRepository.GetFollowedPortfoliosByUserAsync(query.UserId, ct);
        }
    }
}
