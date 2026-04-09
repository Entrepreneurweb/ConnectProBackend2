using ConnectPro.SharedKernel.Exceptions;
using MediatR;
using Portfolio.Domain.Aggregates.Portfolio.Repository;
using Portfolio.Domain.Aggregates.Service.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Services.Commands.Images
{
    public class RemoveImageCommandHandler : IRequestHandler<RemoveImageCommand>
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IServiceRepository _serviceRepository;

        public RemoveImageCommandHandler(
            IPortfolioRepository portfolioRepository,
            IServiceRepository serviceRepository)
        {
            _portfolioRepository = portfolioRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task Handle(RemoveImageCommand command, CancellationToken ct)
        {
            var service = await _serviceRepository.GetByIdWithFullDetailsAsync(command.ServiceId, ct);
            if (service is null)
                throw new DomainException(" service not found");

            var portfolio = await _portfolioRepository.GetByIdAsync(service.PortfolioId, ct);
            if (portfolio!.GetOwnerId != command.RequestingUserId)
                throw new DomainException(" user doesnt own portfolio" );

            service.RemoveImage(command.ImageId);
            await _serviceRepository.UpdateAsync(service, ct);
        }
    }
}
