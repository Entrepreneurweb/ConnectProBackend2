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
    public class UpdateFaqCommandHandler : IRequestHandler<UpdateFaqCommand, FaqDto>
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IServiceRepository _serviceRepository;

        public UpdateFaqCommandHandler(
            IPortfolioRepository portfolioRepository,
            IServiceRepository serviceRepository)
        {
            _portfolioRepository = portfolioRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task<FaqDto> Handle(UpdateFaqCommand command, CancellationToken ct)
        {
            var service = await _serviceRepository.GetByIdWithFullDetailsAsync(command.ServiceId, ct);
            if (service is null)
                throw new DomainException("Service not found");

            var portfolio = await _portfolioRepository.GetByIdAsync(service.PortfolioId, ct);
            if (portfolio!.GetOwnerId != command.RequestingUserId)
                throw new DomainException("User doesnot own the portfolio");

            service.UpdateFaq(command.FaqId, command.Question, command.Answer);
            await _serviceRepository.UpdateAsync(service, ct);

            var faq = service.Faqs.First(f => f.Id.Value == command.FaqId);
            return new FaqDto(faq.Id, faq.Question, faq.Answer);
        }
    }
}
