using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Application.Commands.LikeService
{
    public sealed record LikeServiceCommand(
    Guid UserId,
    Guid ServiceId,
    Guid ServiceOwnerId) : IRequest<Unit>;
}
