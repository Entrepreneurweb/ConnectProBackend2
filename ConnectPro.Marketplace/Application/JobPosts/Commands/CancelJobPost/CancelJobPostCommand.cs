using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Application.JobPosts.Commands.CancelJobPost
{
    public sealed record CancelJobPostCommand(Guid JobPostId) : IRequest;
}
