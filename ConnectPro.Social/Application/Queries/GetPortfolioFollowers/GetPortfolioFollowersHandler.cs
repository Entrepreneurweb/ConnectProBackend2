using MediatR;
using Social.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Application.Queries.GetPortfolioFollowers
{
    public sealed class GetPortfolioFollowersHandler(IFollowRepository followRepository)
        : IRequestHandler<GetPortfolioFollowersQuery, IReadOnlyList<PortfolioFollowerDto>>
    {
        public async Task<IReadOnlyList<PortfolioFollowerDto>> Handle(
            GetPortfolioFollowersQuery query, CancellationToken ct)
        {
            return await followRepository.GetFollowersByPortfolioAsync(query.PortfolioId, ct);
        }
    }
}
