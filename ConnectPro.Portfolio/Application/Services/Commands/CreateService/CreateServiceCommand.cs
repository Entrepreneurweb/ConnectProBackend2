using MediatR;
using Portfolio.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Services.Commands.CreateService
{
    public record CreateServiceCommand(
    Guid PortfolioId,
    Guid RequestingUserId,
    string Title,
    string Description
) : IRequest<ServiceDto>;


}
