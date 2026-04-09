using MediatR;
using Portfolio.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Porfolio.Queries.GetPortfolioByOwner
{
    public record GetPortfolioByOwnerQuery(Guid OwnerId) : IRequest<PortfolioDto>;
}
