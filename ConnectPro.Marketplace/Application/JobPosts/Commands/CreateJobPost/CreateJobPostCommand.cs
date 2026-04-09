using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Application.JobPosts.Commands.CreateJobPost
{
    public sealed record CreateJobPostCommand(
    Guid ClientId,
    string Title,
    string Description,
    decimal Budget
) : IRequest<Guid>;

}
