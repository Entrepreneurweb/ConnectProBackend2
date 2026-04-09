using ConnectPro.SharedKernel.Exceptions;
using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Aggregates.Portfolio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Porfolio.Commands.GeneralInfo
{
    public class UpdateGeneralInfoCommandHandler : IRequestHandler<UpdateGeneralInfoCommand, GeneralInfoDto>
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public UpdateGeneralInfoCommandHandler(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task<GeneralInfoDto> Handle(UpdateGeneralInfoCommand command, CancellationToken ct)
        {
            var portfolio = await _portfolioRepository.GetByIdWithFullDetailsAsync(command.PortfolioId, ct);
            if (portfolio is null)
                throw new DomainException("Portfolio not found");
            if (portfolio.GetOwnerId != command.RequestingUserId)
                throw new DomainException($" User with Id {portfolio.GetOwnerId}  doesn't own the portfolio {portfolio.Id.Value} " );

            portfolio.UpdateGeneralInfo(command.FirstName, command.LastName, command.Bio, command.AvatarUrl);
            await _portfolioRepository.UpdateAsync(portfolio, ct);

            return new GeneralInfoDto(
                portfolio.GeneralInfo!.FirstName,
                portfolio.GeneralInfo!.LastName,
                portfolio.GeneralInfo!.Bio,
                portfolio.GeneralInfo!.AvatarUrl?.Value
            );
        }
    }
}
