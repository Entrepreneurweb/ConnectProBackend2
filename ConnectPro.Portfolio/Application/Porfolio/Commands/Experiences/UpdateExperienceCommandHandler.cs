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
    public class UpdateExperienceCommandHandler : IRequestHandler<UpdateExperienceCommand, ExperienceDto>
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public UpdateExperienceCommandHandler(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task<ExperienceDto> Handle(UpdateExperienceCommand command, CancellationToken ct)
        {
            var portfolio = await _portfolioRepository.GetByIdWithFullDetailsAsync(command.PortfolioId, ct);
            if (portfolio is null)
                throw new DomainException(" portfolio not found");
            if (portfolio.GetOwnerId != command.RequestingUserId)
                throw new DomainException("Users doent own this portfolio");

            portfolio.UpdateExperience(
                command.ExperienceId,
                command.Company,
                command.Role,
                command.Description,
                command.Start,
                command.End
            );

            await _portfolioRepository.UpdateAsync(portfolio, ct);

            var experience = portfolio.Experiences.First(e => e.Id.Value == command.ExperienceId);
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
