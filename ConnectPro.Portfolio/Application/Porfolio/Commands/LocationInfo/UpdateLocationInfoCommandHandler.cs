using ConnectPro.SharedKernel.Exceptions;
using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Aggregates.Portfolio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Porfolio.Commands.LocationInfo
{
    public class UpdateLocationInfoCommandHandler : IRequestHandler<UpdateLocationInfoCommand, LocationInfoDto>
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public UpdateLocationInfoCommandHandler(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task<LocationInfoDto> Handle(UpdateLocationInfoCommand command, CancellationToken ct)
        {
            var portfolio = await _portfolioRepository.GetByIdWithFullDetailsAsync(command.PortfolioId, ct);
            if (portfolio is null)
                throw new DomainException(" portfolio not found");
            if (portfolio.GetOwnerId != command.RequestingUserId)
                throw new DomainException("doesnt own portfolio");

            portfolio.UpdateLocationInfo(command.Country, command.City, command.Timezone);
            await _portfolioRepository.UpdateAsync(portfolio, ct);

            return new LocationInfoDto(
                portfolio.LocationInfo!.Country,
                portfolio.LocationInfo!.City,
                portfolio.LocationInfo!.Timezone
            );
        }
    }
}
