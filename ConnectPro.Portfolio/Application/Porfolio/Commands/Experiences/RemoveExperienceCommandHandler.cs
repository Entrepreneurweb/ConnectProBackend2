using ConnectPro.SharedKernel.Exceptions;
using MediatR;
using Portfolio.Domain.Aggregates.Portfolio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Porfolio.Commands.Experiences
{
    public class RemoveExperienceCommandHandler : IRequestHandler<RemoveExperienceCommand>
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public RemoveExperienceCommandHandler(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task Handle(RemoveExperienceCommand command, CancellationToken ct)
        {
            var portfolio = await _portfolioRepository.GetByIdWithFullDetailsAsync(command.PortfolioId, ct);
            if (portfolio is null)
                throw new DomainException(" portfolio not found");
            if (portfolio.GetOwnerId != command.RequestingUserId)
                throw new DomainException( "user doesnt own the portfolio");

            portfolio.RemoveExperience(command.ExperienceId);
            await _portfolioRepository.UpdateAsync(portfolio, ct);
        }
    }
}
