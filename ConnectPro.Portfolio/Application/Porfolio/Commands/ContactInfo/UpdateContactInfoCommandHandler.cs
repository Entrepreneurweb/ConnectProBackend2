using ConnectPro.SharedKernel.Exceptions;
using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Aggregates.Portfolio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Porfolio.Commands.ContactInfo
{
    public class UpdateContactInfoCommandHandler : IRequestHandler<UpdateContactInfoCommand, ContactInfoDto>
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public UpdateContactInfoCommandHandler(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task<ContactInfoDto> Handle(UpdateContactInfoCommand command, CancellationToken ct)
        {
            var portfolio = await _portfolioRepository.GetByIdWithFullDetailsAsync(command.PortfolioId, ct);
            if (portfolio is null)
                throw new DomainException("Portfolio Not Found");
            if (portfolio.GetOwnerId != command.RequestingUserId)
                throw new DomainException("User  Doesnt not own that portfolio");

            portfolio.UpdateContactInfo(command.Email, command.Phone, command.Website);
            await _portfolioRepository.UpdateAsync(portfolio, ct);

            return new ContactInfoDto(
                portfolio.ContactInfo!.Email.Value,
                portfolio.ContactInfo!.Phone?.Value,
                portfolio.ContactInfo!.Website?.Value
            );
        }
    }
}
