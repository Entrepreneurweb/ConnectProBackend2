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

namespace Portfolio.Application.Services.Commands.Images
{
    public class AddImageCommandHandler : IRequestHandler<AddImageCommand, ImageUrlDto>
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IServiceRepository _serviceRepository;

        public AddImageCommandHandler(
            IPortfolioRepository portfolioRepository,
            IServiceRepository serviceRepository)
        {
            _portfolioRepository = portfolioRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task<ImageUrlDto> Handle(AddImageCommand command, CancellationToken ct)
        {
            var service = await _serviceRepository.GetByIdWithFullDetailsAsync(command.ServiceId, ct);
            if (service is null)
                throw new DomainException("service not found");

            var portfolio = await _portfolioRepository.GetByIdAsync(service.PortfolioId, ct);
            if (portfolio!.GetOwnerId != command.RequestingUserId)
                throw new DomainException("user doesnt own the portfolio");

            var image = service.AddImage(command.Url);
            await _serviceRepository.UpdateAsync(service, ct);

            return new ImageUrlDto(image.Id, image.Value.Value);
        }
    }
}
