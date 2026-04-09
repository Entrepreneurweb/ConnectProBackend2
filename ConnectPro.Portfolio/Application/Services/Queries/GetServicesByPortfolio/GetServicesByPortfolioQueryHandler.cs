using ConnectPro.SharedKernel.Exceptions;
using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Aggregates.Portfolio.Repository;
using Portfolio.Domain.Aggregates.Service.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Services.Queries.GetServicesByPortfolio
{
    public class GetServicesByPortfolioQueryHandler : IRequestHandler<GetServicesByPortfolioQuery, IReadOnlyList<ServiceDto>>
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IServiceRepository _serviceRepository;

        public GetServicesByPortfolioQueryHandler(
            IPortfolioRepository portfolioRepository,
            IServiceRepository serviceRepository)
        {
            _portfolioRepository = portfolioRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task<IReadOnlyList<ServiceDto>> Handle(GetServicesByPortfolioQuery query, CancellationToken ct)
        {
            var portfolio = await _portfolioRepository.GetByIdAsync(query.PortfolioId, ct);
            if (portfolio is null)
                throw new DomainException(" porfolio doenst exist");

            var services = await _serviceRepository.GetByPortfolioIdAsync(query.PortfolioId, ct);

            return services
                .Select(s => new ServiceDto(
                    s.Id,
                    s.PortfolioId,
                    s.GetTitle,
                    s.GetDescription,
                    s.Status.ToString(),
                    s.CoverImageUrl?.Value
                ))
                .ToList();
        }
    }
}
