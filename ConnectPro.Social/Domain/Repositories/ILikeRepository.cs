using Social.Application.Dtos;
using Social.Domain.Aggregates.Like.Entities;
using Social.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Domain.Repositories
{
    public interface ILikeRepository
    {
        Task<Like?> GetAsync(Guid userId, Guid serviceId, CancellationToken ct = default);
        Task AddAsync(Like like, CancellationToken ct = default);
        Task RemoveAsync(Like like, CancellationToken ct = default);
        Task<IReadOnlyList<ServiceLikeDto>> GetLikesByServiceAsync(Guid serviceId, CancellationToken ct = default);
        Task<IReadOnlyList<LikedServiceDto>> GetLikedServicesByUserAsync(Guid userId, CancellationToken ct = default);
    }
}
