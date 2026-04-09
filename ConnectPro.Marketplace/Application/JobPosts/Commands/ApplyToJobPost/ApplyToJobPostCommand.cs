using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Application.JobPosts.Commands.ApplyToJobPost
{
    public sealed record ApplyToJobPostCommand(
    Guid JobPostId,
    Guid FreelancerId,
    string CoverLetter,
    decimal ProposedRate
) : IRequest<Guid>;

}
