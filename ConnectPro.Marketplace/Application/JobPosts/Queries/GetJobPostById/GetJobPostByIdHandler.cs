using Marketplace.Application.Dtos;
using Marketplace.Domain.Aggregates.JobPost.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Application.JobPosts.Queries.GetJobPostById
{
    public sealed class GetJobPostByIdHandler : IRequestHandler<GetJobPostByIdQuery, JobPostDto?>
    {
        private readonly IJobPostRepository _jobPostRepository;

        public GetJobPostByIdHandler(IJobPostRepository jobPostRepository)
            => _jobPostRepository = jobPostRepository;

        public async Task<JobPostDto?> Handle(GetJobPostByIdQuery query, CancellationToken cancellationToken)
        {
            var jobPost = await _jobPostRepository.GetByIdAsync(query.JobPostId, cancellationToken);

            if (jobPost is null) return null;

            return new JobPostDto(
                jobPost.Id,
                jobPost.ClientId,
                jobPost.Title,
                jobPost.Description,
                jobPost.Budget.Amount,
                jobPost.Status.ToString(),
                jobPost.Applications.Select(a => new ApplicationDto(
                    a.Id,
                    a.FreelancerId,
                    a.CoverLetter,
                    a.ProposedRate.Amount,
                    a.Status.ToString()
                )).ToList()
            );
        }
    }

}
