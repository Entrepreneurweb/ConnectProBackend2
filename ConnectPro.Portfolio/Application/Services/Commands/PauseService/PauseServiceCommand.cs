using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Services.Commands.PauseService
{
    public record PauseServiceCommand(
    Guid ServiceId,
    Guid RequestingUserId
) : IRequest;
}
