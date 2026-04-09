using Microsoft.EntityFrameworkCore;
using Social.Application.Dtos;
using Social.Domain.Aggregates.Like.Entities;
using Social.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Infrastructure.Persistence.Repositories
{
    public sealed class LikeRepository(SocialGraphDbContext context) : ILikeRepository
    {
        public async Task<Like?> GetAsync(Guid userId, Guid serviceId, CancellationToken ct = default)
        {
            return await context.Likes
                .FirstOrDefaultAsync(l => l.UserId == userId && l.ServiceId == serviceId, ct);
        }

        public async Task AddAsync(Like like, CancellationToken ct = default)
        {
            await context.Likes.AddAsync(like, ct);
            await context.SaveChangesAsync(ct);
        }

        public async Task RemoveAsync(Like like, CancellationToken ct = default)
        {
            context.Likes.Remove(like);
            await context.SaveChangesAsync(ct);
        }

        public async Task<IReadOnlyList<ServiceLikeDto>> GetLikesByServiceAsync(
            Guid serviceId, CancellationToken ct = default)
        {
            return await context.Likes
                .Where(l => l.ServiceId == serviceId)
                .Select(l => new ServiceLikeDto(l.UserId, l.CreatedAt))
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<LikedServiceDto>> GetLikedServicesByUserAsync(
            Guid userId, CancellationToken ct = default)
        {
            return await context.Likes
                .Where(l => l.UserId == userId)
                .Select(l => new LikedServiceDto(l.ServiceId, l.CreatedAt))
                .ToListAsync(ct);
        }
    }
}
