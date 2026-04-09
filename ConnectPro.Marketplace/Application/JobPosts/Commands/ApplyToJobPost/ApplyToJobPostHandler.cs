using Marketplace.Domain.Aggregates.JobPost.Repository;
using Marketplace.Domain.Aggregates.JobPost.ValueObject;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Application.JobPosts.Commands.ApplyToJobPost
{
    public sealed class ApplyToJobPostHandler : IRequestHandler<ApplyToJobPostCommand, Guid>
    {
        private readonly IJobPostRepository _jobPostRepository;
        private readonly IPublisher _publisher;

        public ApplyToJobPostHandler(IJobPostRepository jobPostRepository, IPublisher publisher)
        {
            _jobPostRepository = jobPostRepository;
            _publisher = publisher;
        }

        public async Task<Guid> Handle(ApplyToJobPostCommand command, CancellationToken cancellationToken)
        {
            var jobPost = await _jobPostRepository.GetByIdAsync(command.JobPostId, cancellationToken)
                ?? throw new InvalidOperationException($"JobPost {command.JobPostId} not found.");

            var application = jobPost.Apply(
                command.FreelancerId,
                command.CoverLetter,
                ProposedRate.Create(command.ProposedRate));

            await _jobPostRepository.UpdateAsync(jobPost, cancellationToken);

            foreach (var domainEvent in jobPost.DomainEvents)
                await _publisher.Publish(domainEvent, cancellationToken);

           // jobPost.ClearDomainEvents();

            return application.Id;
        }
    }
}
