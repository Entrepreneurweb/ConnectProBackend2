using MediatR;
using Portfolio.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Porfolio.Commands.CreatePortfolio
{
    public record CreatePortfolioCommand(
     Guid OwnerId,
     string Type
 ) : IRequest<PortfolioDto>;
}
