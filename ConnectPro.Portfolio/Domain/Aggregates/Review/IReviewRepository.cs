using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Aggregates.Review
{
    public interface IReviewRepository
    {
        Task<Review?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IReadOnlyList<Review>> GetByServiceIdAsync(Guid serviceId, CancellationToken ct = default);
        Task<Review?> GetByServiceAndReviewerAsync(Guid serviceId, Guid reviewerId, CancellationToken ct = default);
        Task AddAsync(Review review, CancellationToken ct = default);
        Task UpdateAsync(Review review, CancellationToken ct = default);
        Task DeleteAsync(Review review, CancellationToken ct = default);
    }
}
