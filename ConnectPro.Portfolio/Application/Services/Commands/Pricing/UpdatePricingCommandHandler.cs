using ConnectPro.SharedKernel.Exceptions;
using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Aggregates.Portfolio.Repository;
using Portfolio.Domain.Aggregates.Service.Repository;
using Portfolio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Portfolio.Application.Services.Commands.Pricing
{

    public class UpdatePricingCommandHandler : IRequestHandler<UpdatePricingCommand, PricingDto>
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IServiceRepository _serviceRepository;

        public UpdatePricingCommandHandler(
            IPortfolioRepository portfolioRepository,
            IServiceRepository serviceRepository)
        {
            _portfolioRepository = portfolioRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task<PricingDto> Handle(UpdatePricingCommand command, CancellationToken ct)
        {
            var service = await _serviceRepository.GetByIdAsync(command.ServiceId, ct);
            if (service is null)
                throw new DomainException("service not found");

            var portfolio = await _portfolioRepository.GetByIdAsync(service.PortfolioId, ct);
            if (portfolio!.GetOwnerId != command.RequestingUserId)
                throw new DomainException(" user doesnt own the portfolio");

             if(!Enum.IsDefined(typeof(PricingType), command.Type))
                throw new DomainException("invalid pricing type");

            service.UpdatePricing(command.Amount, command.Currency, PricingType.Hourly);
            await _serviceRepository.UpdateAsync(service, ct);

            return new PricingDto(
                service.Pricing!.Amount,
                service.Pricing!.currency.ToString() ,
                service.Pricing!.Type.ToString()
            );
        }
    }
}
