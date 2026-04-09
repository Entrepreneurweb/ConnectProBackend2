using MediatR;
using Portfolio.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Services.Commands.Awards
{
    public record AddAwardCommand(
     Guid ServiceId,
     Guid RequestingUserId,
     string Title,
     string IssuedBy,
     DateOnly IssuedAt,
     string? Description
 ) : IRequest<AwardDto>;

}
