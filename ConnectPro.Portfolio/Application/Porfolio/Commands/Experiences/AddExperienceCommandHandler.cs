using ConnectPro.SharedKernel.Exceptions;
using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Aggregates.Portfolio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Porfolio.Commands.Experiences
{
    public class AddExperienceCommandHandler : IRequestHandler<AddExperienceCommand, ExperienceDto>
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public AddExperienceCommandHandler(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task<ExperienceDto> Handle(AddExperienceCommand command, CancellationToken ct)
        {
            var portfolio = await _portfolioRepository.GetByIdWithFullDetailsAsync(command.PortfolioId, ct);
            if (portfolio is null)
                throw new DomainException("Portfolio not found");
            if (portfolio.GetOwnerId != command.RequestingUserId)
                throw new DomainException(" the user Doesnt own this portfolio");

            var experience = portfolio.AddExperience(
                command.Company,
                command.Role,
                command.Description,
                command.Start,
                command.End
            );

            await _portfolioRepository.UpdateAsync(portfolio, ct);

            return new ExperienceDto(
                experience.Id,
                experience.Company,
                experience.Role,
                experience.Description,
                experience.Period.Start,
                experience.Period.End
            );
        }
    }
}
