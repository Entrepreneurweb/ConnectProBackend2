using Marketplace.Domain.Aggregates.JobPost.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Application.JobPosts.Commands.AcceptApplication
{
    public sealed class AcceptApplicationHandler : IRequestHandler<AcceptApplicationCommand>
    {
        private readonly IJobPostRepository _jobPostRepository;
        private readonly IPublisher _publisher;

        public AcceptApplicationHandler(IJobPostRepository jobPostRepository, IPublisher publisher)
        {
            _jobPostRepository = jobPostRepository;
            _publisher = publisher;
        }

        public async Task Handle(AcceptApplicationCommand command, CancellationToken cancellationToken)
        {
            var jobPost = await _jobPostRepository.GetByIdAsync(command.JobPostId, cancellationToken)
                ?? throw new InvalidOperationException($"JobPost {command.JobPostId} not found.");

            jobPost.AcceptApplication(command.ApplicationId);

            await _jobPostRepository.UpdateAsync(jobPost, cancellationToken);

            foreach (var domainEvent in jobPost.DomainEvents)
                await _publisher.Publish(domainEvent, cancellationToken);

           // jobPost.ClearDomainEvents();
        }
    }
}
