using Marketplace.Domain.Aggregates.JobPost.Entities;
using Marketplace.Domain.Aggregates.JobPost.Repository;
using Marketplace.Domain.Aggregates.JobPost.ValueObject;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Application.JobPosts.Commands.CreateJobPost
{
    public sealed class CreateJobPostHandler : IRequestHandler<CreateJobPostCommand, Guid>
    {
        private readonly IJobPostRepository _jobPostRepository;
        private readonly IPublisher _publisher;

        public CreateJobPostHandler(IJobPostRepository jobPostRepository, IPublisher publisher)
        {
            _jobPostRepository = jobPostRepository;
            _publisher = publisher;
        }

        public async Task<Guid> Handle(CreateJobPostCommand command, CancellationToken cancellationToken)
        {
            var jobPost = JobPost.Create(
                command.ClientId,
                command.Title,
                command.Description,
                Budget.Create(command.Budget));

            await _jobPostRepository.AddAsync(jobPost, cancellationToken);

            foreach (var domainEvent in jobPost.DomainEvents)
                await _publisher.Publish(domainEvent, cancellationToken);

            //jobPost.ClearDomainEvents();

            return jobPost.Id;
        }
    }
}
