using MediatR;
using Portfolio.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Porfolio.Commands.GeneralInfo
{
    public record UpdateGeneralInfoCommand(
     Guid PortfolioId,
     Guid RequestingUserId,
     string FirstName,
     string LastName,
     string? Bio,
     string? AvatarUrl
 ) : IRequest<GeneralInfoDto>;
}
