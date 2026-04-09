using Microsoft.EntityFrameworkCore;
using Social.Application.Queries.GetPortfolioFollowers;
using Social.Application.Queries.GetUserFollowedPortfolios;
using Social.Domain.Aggregates.Follow.Entities;
using Social.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Infrastructure.Persistence.Repositories
{
    public sealed class FollowRepository(SocialGraphDbContext context) : IFollowRepository
    {
        public async Task<Follow?> GetAsync(Guid followerId, Guid portfolioId, CancellationToken ct = default)
        {
            return await context.Follows
                .FirstOrDefaultAsync(f => f.FollowerId == followerId && f.PortfolioId == portfolioId, ct);
        }

        public async Task AddAsync(Follow follow, CancellationToken ct = default)
        {
            await context.Follows.AddAsync(follow, ct);
            await context.SaveChangesAsync(ct);
        }

        public async Task RemoveAsync(Follow follow, CancellationToken ct = default)
        {
            context.Follows.Remove(follow);
            await context.SaveChangesAsync(ct);
        }

        public async Task<IReadOnlyList<PortfolioFollowerDto>> GetFollowersByPortfolioAsync(
            Guid portfolioId, CancellationToken ct = default)
        {
            return await context.Follows
                .Where(f => f.PortfolioId == portfolioId)
                .Select(f => new PortfolioFollowerDto(f.FollowerId, f.CreatedAt))
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<FollowedPortfolioDto>> GetFollowedPortfoliosByUserAsync(
            Guid userId, CancellationToken ct = default)
        {
            return await context.Follows
                .Where(f => f.FollowerId == userId)
                .Select(f => new FollowedPortfolioDto(f.PortfolioId, f.CreatedAt))
                .ToListAsync(ct);
        }
    }
}
