using Social.Application.Queries.GetPortfolioFollowers;
using Social.Application.Queries.GetUserFollowedPortfolios;
using Social.Domain.Aggregates.Follow.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Domain.Repositories
{
    public interface IFollowRepository
    {
        Task<Follow?> GetAsync(Guid followerId, Guid portfolioId, CancellationToken ct = default);
        Task AddAsync(Follow follow, CancellationToken ct = default);
        Task RemoveAsync(Follow follow, CancellationToken ct = default);
        Task<IReadOnlyList<PortfolioFollowerDto>> GetFollowersByPortfolioAsync(Guid portfolioId, CancellationToken ct = default);
        Task<IReadOnlyList<FollowedPortfolioDto>> GetFollowedPortfoliosByUserAsync(Guid userId, CancellationToken ct = default);
    }
}
