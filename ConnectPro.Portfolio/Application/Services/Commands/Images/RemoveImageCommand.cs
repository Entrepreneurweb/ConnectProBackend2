using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Services.Commands.Images
{
    public record RemoveImageCommand(
    Guid ServiceId,
    Guid RequestingUserId,
    Guid ImageId
) : IRequest;

}
