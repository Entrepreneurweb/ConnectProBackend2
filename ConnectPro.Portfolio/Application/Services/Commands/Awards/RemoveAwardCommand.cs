using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Services.Commands.Awards
{
    public record RemoveAwardCommand(
     Guid ServiceId,
     Guid RequestingUserId,
     Guid AwardId
 ) : IRequest;

}
