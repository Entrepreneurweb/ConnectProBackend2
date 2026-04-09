using ConnectPro.SharedKernel.Exceptions;
using MediatR;
using Portfolio.Domain.Aggregates.Portfolio.Repository;
using Portfolio.Domain.Aggregates.Service.Repository;
using Portfolio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Services.Commands.DeleteService
{
    public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand>
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IServiceRepository _serviceRepository;

        public DeleteServiceCommandHandler(
            IPortfolioRepository portfolioRepository,
            IServiceRepository serviceRepository)
        {
            _portfolioRepository = portfolioRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task Handle(DeleteServiceCommand command, CancellationToken ct)
        {
            var service = await _serviceRepository.GetByIdAsync(command.ServiceId, ct);
            if (service is null)
                throw new DomainException(" service not found" );

            var portfolio = await _portfolioRepository.GetByIdAsync(service.PortfolioId, ct);
            if (portfolio!.GetOwnerId != command.RequestingUserId)
                throw new DomainException(" user doesn't own the portfolio" );

            if (service.Status == ServiceStatus.Published)
                portfolio.DecrementActiveServices();

            await _serviceRepository.DeleteAsync(service, ct);
            await _portfolioRepository.UpdateAsync(portfolio, ct);
        }
    }
}
