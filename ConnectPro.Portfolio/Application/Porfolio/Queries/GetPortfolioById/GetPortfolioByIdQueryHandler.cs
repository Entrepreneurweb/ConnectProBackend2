using ConnectPro.SharedKernel.Exceptions;
using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Aggregates.Portfolio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Porfolio.Queries.GetPortfolioById
{
    public class GetPortfolioByIdQueryHandler : IRequestHandler<GetPortfolioByIdQuery, PortfolioDto>
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public GetPortfolioByIdQueryHandler(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task<PortfolioDto> Handle(GetPortfolioByIdQuery query, CancellationToken ct)
        {
            var portfolio = await _portfolioRepository.GetByIdAsync(query.PortfolioId, ct);
            if (portfolio is null)
                throw new DomainException(" portfolio not found");

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
