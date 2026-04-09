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

namespace Portfolio.Application.Services.Commands.Awards
{
   

    public class UpdateAwardCommandHandler : IRequestHandler<UpdateAwardCommand, AwardDto>
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IServiceRepository _serviceRepository;

        public UpdateAwardCommandHandler(
            IPortfolioRepository portfolioRepository,
            IServiceRepository serviceRepository)
        {
            _portfolioRepository = portfolioRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task<AwardDto> Handle(UpdateAwardCommand command, CancellationToken ct)
        {
            var service = await _serviceRepository.GetByIdWithFullDetailsAsync(command.ServiceId, ct);
            if (service is null)
                throw new DomainException( "service not found" );

            var portfolio = await _portfolioRepository.GetByIdAsync(service.PortfolioId, ct);
            if (portfolio!.GetOwnerId != command.RequestingUserId)
                throw new DomainException(" user doesnt own the portfolio");

            service.UpdateAward(command.AwardId, command.Title, command.IssuedBy, command.IssuedAt, command.Description);
            await _serviceRepository.UpdateAsync(service, ct);

            var award = service.Awards.First(a => a.Id.Value == command.AwardId);
            return new AwardDto(award.Id, award.Title, award.IssuedBy, award.IssuedAt, award.Description);
        }
    }

}
