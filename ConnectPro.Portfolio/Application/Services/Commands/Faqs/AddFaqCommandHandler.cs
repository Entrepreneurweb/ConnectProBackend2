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

namespace Portfolio.Application.Services.Commands.Faqs
{
    public class AddFaqCommandHandler : IRequestHandler<AddFaqCommand, FaqDto>
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IServiceRepository _serviceRepository;

        public AddFaqCommandHandler(
            IPortfolioRepository portfolioRepository,
            IServiceRepository serviceRepository)
        {
            _portfolioRepository = portfolioRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task<FaqDto> Handle(AddFaqCommand command, CancellationToken ct)
        {
            var service = await _serviceRepository.GetByIdWithFullDetailsAsync(command.ServiceId, ct);
            if (service is null)
                throw new DomainException(" Service not found");

            var portfolio = await _portfolioRepository.GetByIdAsync(service.PortfolioId, ct);
            if (portfolio!.GetOwnerId != command.RequestingUserId)
                throw new DomainException( "User doesnt own this portfolio ");

            var faq = service.AddFaq(command.Question, command.Answer);
            await _serviceRepository.UpdateAsync(service, ct);

            return new FaqDto(faq.Id, faq.Question, faq.Answer);
        }
    }
}
