using ConnectPro.SharedKernel.Exceptions;
using MediatR;
using Portfolio.Domain.Aggregates.Portfolio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Porfolio.Commands.Certifications
{
    public class RemoveCertificationCommandHandler : IRequestHandler<RemoveCertificationCommand>
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public RemoveCertificationCommandHandler(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task Handle(RemoveCertificationCommand command, CancellationToken ct)
        {
            var portfolio = await _portfolioRepository.GetByIdWithFullDetailsAsync(command.PortfolioId, ct);
            if (portfolio is null)
                throw new DomainException( " portfolio not found");
            if (portfolio.GetOwnerId != command.RequestingUserId)
                throw new DomainException( " user doent own the portfolio");

            portfolio.RemoveCertification(command.CertificationId);
            await _portfolioRepository.UpdateAsync(portfolio, ct);
        }
    }
}
