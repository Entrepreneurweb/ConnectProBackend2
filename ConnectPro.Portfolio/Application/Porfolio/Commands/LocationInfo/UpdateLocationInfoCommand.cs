using MediatR;
using Portfolio.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Porfolio.Commands.LocationInfo
{
    public record UpdateLocationInfoCommand(
    Guid PortfolioId,
    Guid RequestingUserId,
    string Country,
    string City,
    string? Timezone
) : IRequest<LocationInfoDto>;

}
