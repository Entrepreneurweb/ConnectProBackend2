using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Application.JobPosts.Commands.AcceptApplication
{
    public sealed record AcceptApplicationCommand(
    Guid JobPostId,
    Guid ApplicationId
) : IRequest;

}
