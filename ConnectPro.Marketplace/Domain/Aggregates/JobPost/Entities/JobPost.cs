using ConnectPro.SharedKernel;
using Marketplace.Domain.Aggregates.JobPost.ValueObject;
using Marketplace.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain.Aggregates.JobPost.Entities
{

    public sealed class JobPost : AggregateRoot<JobPost>
    {
        private readonly List<Application> _applications = new();

        public Guid ClientId { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public Budget Budget { get; private set; } = null!;
        public JobPostStatus Status { get; private set; }

        public IReadOnlyCollection<Application> Applications => _applications.AsReadOnly();

        private JobPost() { }

        public static JobPost Create(Guid clientId, string title, string description, Budget budget)
        {
            if (clientId == Guid.Empty)
                throw new ArgumentException("ClientId is required.", nameof(clientId));

            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title is required.", nameof(title));

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description is required.", nameof(description));

            var jobPost = new JobPost
            {
                Id = Guid.NewGuid(),
                ClientId = clientId,
                Title = title,
                Description = description,
                Budget = budget,
                Status = JobPostStatus.Open
            };

          //  jobPost.RaiseDomainEvent(new JobPostCreated(Guid.NewGuid(), DateTime.UtcNow, jobPost.Id, clientId, title));

            return jobPost;
        }

        public Application Apply(Guid freelancerId, string coverLetter, ProposedRate proposedRate)
        {
            if (Status != JobPostStatus.Open)
                throw new InvalidOperationException("Cannot apply to a job post that is not open.");

            if (_applications.Any(a => a.FreelancerId == freelancerId))
                throw new InvalidOperationException("Freelancer has already applied to this job post.");

            var application = Application.Create(freelancerId, coverLetter, proposedRate);
            _applications.Add(application);

         //   RaiseDomainEvent(new ApplicationSubmitted(Guid.NewGuid(), DateTime.UtcNow, application.Id, Id, freelancerId));

            return application;
        }

        public void AcceptApplication(Guid applicationId)
        {
            if (Status != JobPostStatus.Open)
                throw new InvalidOperationException("Job post is no longer open.");

            var application = _applications.FirstOrDefault(a => a.Id.Value == applicationId)
                ?? throw new InvalidOperationException($"Application {applicationId} not found.");

            application.Accept();
         //   RaiseDomainEvent(new ApplicationAccepted(Guid.NewGuid(), DateTime.UtcNow, application.Id, Id, application.FreelancerId));

            /*
            foreach (var other in _applications.Where(a => a.Id.Value != applicationId))
            {
                other.Reject();
                RaiseDomainEvent(new ApplicationRejected(Guid.NewGuid(), DateTime.UtcNow, other.Id, Id, other.FreelancerId));
            }
            */

            Status = JobPostStatus.Closed;
          //  RaiseDomainEvent(new JobPostClosed(Guid.NewGuid(), DateTime.UtcNow, Id));
        }

        public void Cancel()
        {
            if (Status == JobPostStatus.Closed)
                throw new InvalidOperationException("Cannot cancel a closed job post.");

            Status = JobPostStatus.Cancelled;
        }
    }
}
