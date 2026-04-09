using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobPostEntity = Marketplace.Domain.Aggregates.JobPost.Entities.JobPost;

namespace Marketplace.Domain.Aggregates.JobPost.Repository
{
    public interface IJobPostRepository
    {
        Task<JobPostEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task AddAsync(JobPostEntity jobPost, CancellationToken cancellationToken = default);
        Task UpdateAsync(JobPostEntity jobPost, CancellationToken cancellationToken = default);
    }
}
