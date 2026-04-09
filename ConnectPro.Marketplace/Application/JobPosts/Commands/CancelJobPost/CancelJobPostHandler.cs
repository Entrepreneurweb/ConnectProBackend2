using Marketplace.Domain.Aggregates.JobPost.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Application.JobPosts.Commands.CancelJobPost
{
    public sealed class CancelJobPostHandler : IRequestHandler<CancelJobPostCommand>
    {
        private readonly IJobPostRepository _jobPostRepository;
        private readonly IPublisher _publisher;

        public CancelJobPostHandler(IJobPostRepository jobPostRepository, IPublisher publisher)
        {
            _jobPostRepository = jobPostRepository;
            _publisher = publisher;
        }

        public async Task Handle(CancelJobPostCommand command, CancellationToken cancellationToken)
        {
            var jobPost = await _jobPostRepository.GetByIdAsync(command.JobPostId, cancellationToken)
                ?? throw new InvalidOperationException($"JobPost {command.JobPostId} not found.");

            jobPost.Cancel();

            await _jobPostRepository.UpdateAsync(jobPost, cancellationToken);

            foreach (var domainEvent in jobPost.DomainEvents)
                await _publisher.Publish(domainEvent, cancellationToken);

           // jobPost.ClearDomainEvents();
        }
    }
}
