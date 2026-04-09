using MediatR;
using Portfolio.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Porfolio.Commands.Experiences
{
    public record UpdateExperienceCommand(
     Guid PortfolioId,
     Guid RequestingUserId,
     Guid ExperienceId,
     string Company,
     string Role,
     string? Description,
     DateOnly Start,
     DateOnly? End
 ) : IRequest<ExperienceDto>;
}
