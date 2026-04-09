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

    public sealed class Application : Entity<Application>
    {
        public Guid FreelancerId { get; private set; }
        public string CoverLetter { get; private set; } = string.Empty;
        public Id<JobPost> JobPostId { get; private set; }
        public ProposedRate ProposedRate { get; private set; } = null!;
        public ApplicationStatus Status { get; private set; }

        private Application() { }

        internal static Application Create(
            Guid freelancerId,
            string coverLetter,
            ProposedRate proposedRate)
        {
            if (freelancerId == Guid.Empty)
                throw new ArgumentException("FreelancerId is required.", nameof(freelancerId));

            if (string.IsNullOrWhiteSpace(coverLetter))
                throw new ArgumentException("Cover letter is required.", nameof(coverLetter));

            return new Application
            {
               // Id = Guid.NewGuid(),
                FreelancerId = freelancerId,
                CoverLetter = coverLetter,
                ProposedRate = proposedRate,
                Status = ApplicationStatus.Pending
            };
        }

        internal void Accept() => Status = ApplicationStatus.Accepted;
        internal void Reject() => Status = ApplicationStatus.Rejected;
    }
}
