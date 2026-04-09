using ConnectPro.SharedKernel.Exceptions;
using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Aggregates.Portfolio.Repository;
using Portfolio.Domain.Aggregates.Service.Entities;
using Portfolio.Domain.Aggregates.Service.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Services.Commands.CreateService
{
    public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, ServiceDto>
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IServiceRepository _serviceRepository;

        public CreateServiceCommandHandler(
            IPortfolioRepository portfolioRepository,
            IServiceRepository serviceRepository)
        {
            _portfolioRepository = portfolioRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task<ServiceDto> Handle(CreateServiceCommand command, CancellationToken ct)
        {
            var portfolio = await _portfolioRepository.GetByIdAsync(command.PortfolioId, ct);
            if (portfolio is null)
                throw new DomainException("portfolio not found");
            if (portfolio.GetOwnerId != command.RequestingUserId)
                throw new DomainException("user doesnot own the portfolio");

            portfolio.IncrementActiveServices();

            var service = Service.Create(command.PortfolioId, command.Title, command.Description);

            await _serviceRepository.AddAsync(service, ct);
            await _portfolioRepository.UpdateAsync(portfolio, ct);

            return new ServiceDto(
                service.Id,
                service.PortfolioId,
                service.GetTitle,
                service.GetDescription,
                service.Status.ToString(),
                service.CoverImageUrl?.Value
            );
        }
    }

}
