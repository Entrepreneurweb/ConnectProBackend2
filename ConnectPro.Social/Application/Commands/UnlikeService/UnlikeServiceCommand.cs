using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Application.Commands.UnlikeService
{
    public sealed record UnlikeServiceCommand(
    Guid UserId,
    Guid ServiceId) : IRequest<Unit>;
}
