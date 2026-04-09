using ConnectPro.SharedKernel.Exceptions;
using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Aggregates.Portfolio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Porfolio.Queries.GetPortfolioByOwner
{
    public class GetPortfolioByOwnerQueryHandler : IRequestHandler<GetPortfolioByOwnerQuery, PortfolioDto>
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public GetPortfolioByOwnerQueryHandler(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task<PortfolioDto> Handle(GetPortfolioByOwnerQuery query, CancellationToken ct)
        {
            var portfolio = await _portfolioRepository.GetByOwnerIdAsync(query.OwnerId, ct);
            if (portfolio is null)
                throw new DomainException( " Portfolio not found"  ) ;

            return new PortfolioDto(
                portfolio.Id,
                portfolio.GetOwnerId,
                portfolio.GetPortfolioType.ToString(),
                portfolio.Status.ToString(),
                portfolio.ActiveServicesCount
            );
        }
    }
}
