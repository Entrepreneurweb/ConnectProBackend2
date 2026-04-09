using Marketplace.Domain.Aggregates.JobPost.Entities;
using Marketplace.Domain.Aggregates.JobPost.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Infrastructure.Persistence.Repositories
{
    public sealed class JobPostRepository : IJobPostRepository
    {
        private readonly MarketPlaceDbContext _context;

        public JobPostRepository(MarketPlaceDbContext context)
            => _context = context;

        public async Task<JobPost?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _context.JobPosts
                .FirstOrDefaultAsync(j => j.Id.Value == id, cancellationToken);

        public async Task AddAsync(JobPost jobPost, CancellationToken cancellationToken = default)
        {
            await _context.JobPosts.AddAsync(jobPost, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(JobPost jobPost, CancellationToken cancellationToken = default)
        {
            _context.JobPosts.Update(jobPost);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
