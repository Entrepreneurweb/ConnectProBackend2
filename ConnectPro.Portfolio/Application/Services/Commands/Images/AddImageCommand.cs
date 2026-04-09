using MediatR;
using Portfolio.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Services.Commands.Images
{
    public record AddImageCommand(
     Guid ServiceId,
     Guid RequestingUserId,
     string Url
 ) : IRequest<ImageUrlDto>;

}
