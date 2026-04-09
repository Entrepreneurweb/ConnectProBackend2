using MediatR;
using Portfolio.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Services.Commands.Tags
{
    public record AddTagCommand(
    Guid ServiceId,
    Guid RequestingUserId,
    string Value
) : IRequest<TagDto>;

}
