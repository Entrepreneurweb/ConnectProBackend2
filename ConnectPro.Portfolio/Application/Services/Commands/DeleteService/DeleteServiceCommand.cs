using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Services.Commands.DeleteService
{
    public record DeleteServiceCommand(
     Guid ServiceId,
     Guid RequestingUserId
 ) : IRequest;
}
