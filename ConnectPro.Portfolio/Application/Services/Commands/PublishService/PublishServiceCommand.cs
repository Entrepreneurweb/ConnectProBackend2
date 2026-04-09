using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Services.Commands.PublishService
{
    public record PublishServiceCommand(
     Guid ServiceId,
     Guid RequestingUserId
 ) : IRequest;

}
