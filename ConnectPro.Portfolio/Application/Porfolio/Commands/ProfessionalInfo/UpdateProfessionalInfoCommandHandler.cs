using ConnectPro.SharedKernel.Exceptions;
using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Aggregates.Portfolio.Repository;
using Portfolio.Domain.Aggregates.Portfolio.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Porfolio.Commands.ProfessionalInfo
{
    public class UpdateProfessionalInfoCommandHandler : IRequestHandler<UpdateProfessionalInfoCommand, ProfessionalInfoDto>
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public UpdateProfessionalInfoCommandHandler(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task<ProfessionalInfoDto> Handle(UpdateProfessionalInfoCommand command, CancellationToken ct)
        {
            var portfolio = await _portfolioRepository.GetByIdWithFullDetailsAsync(command.PortfolioId, ct);
            if (portfolio is null)
                throw new DomainException(" portfolio not found");
            if (portfolio.GetOwnerId != command.RequestingUserId)
                throw new DomainException("User Doesnt own that portfolio");

            var skills = command.Skills
                .Select(s => Skill.Create(s.Name, s.Level))
                .ToList();

            portfolio.UpdateProfessionalInfo(command.Headline, skills);
            await _portfolioRepository.UpdateAsync(portfolio, ct);

            return new ProfessionalInfoDto(
                portfolio.ProfessionalInfo!.Headline,
                portfolio.ProfessionalInfo!.Skills
                    .Select(s => new SkillDto(s.Name, s.Level))
                    .ToList()
            );
        }
    }
}
