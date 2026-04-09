using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Services.Commands.Tags
{
    public record RemoveTagCommand(
     Guid ServiceId,
     Guid RequestingUserId,
     Guid TagId
 ) : IRequest;

}
