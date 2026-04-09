using MediatR;
using Portfolio.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Services.Commands.UpdateService
{
    public record UpdateServiceCommand(
    Guid ServiceId,
    Guid RequestingUserId,
    string Title,
    string Description,
    string? CoverImageUrl
) : IRequest<ServiceDto>;


}
