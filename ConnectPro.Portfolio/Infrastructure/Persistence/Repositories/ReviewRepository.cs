using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Aggregates.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Infrastructure.Persistence.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly PortfolioDbContext _context;

        public ReviewRepository(PortfolioDbContext context)
        {
            _context = context;
        }

        public async Task<Review?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Reviews
                .FirstOrDefaultAsync(r => r.Id.Value == id, ct);
        }

        public async Task<IReadOnlyList<Review>> GetByServiceIdAsync(Guid serviceId, CancellationToken ct = default)
        {
            return await _context.Reviews
                .Where(r => EF.Property<Guid>(r, "_serviceId") == serviceId)
                .ToListAsync(ct);
        }

        public async Task<Review?> GetByServiceAndReviewerAsync(Guid serviceId, Guid reviewerId, CancellationToken ct = default)
        {
            return await _context.Reviews
                .FirstOrDefaultAsync(r =>
                    EF.Property<Guid>(r, "_serviceId") == serviceId &&
                    EF.Property<Guid>(r, "_reviewerId") == reviewerId, ct);
        }

        public async Task AddAsync(Review review, CancellationToken ct = default)
        {
            await _context.Reviews.AddAsync(review, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Review review, CancellationToken ct = default)
        {
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Review review, CancellationToken ct = default)
        {
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync(ct);
        }
    }
}
