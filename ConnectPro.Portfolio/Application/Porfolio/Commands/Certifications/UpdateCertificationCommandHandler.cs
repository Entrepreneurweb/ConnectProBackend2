using ConnectPro.SharedKernel.Exceptions;
using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Aggregates.Portfolio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Porfolio.Commands.Certifications
{
    public class UpdateCertificationCommandHandler : IRequestHandler<UpdateCertificationCommand, CertificationDto>
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public UpdateCertificationCommandHandler(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task<CertificationDto> Handle(UpdateCertificationCommand command, CancellationToken ct)
        {
            var portfolio = await _portfolioRepository.GetByIdWithFullDetailsAsync(command.PortfolioId, ct);
            if (portfolio is null)
                throw new DomainException(" portfolio not found" );
            if (portfolio.GetOwnerId != command.RequestingUserId)
                throw new DomainException( " user doen't own the portfolio");

            portfolio.UpdateCertification(
                command.CertificationId,
                command.Name,
                command.IssuingOrganization,
                command.IssueDate,
                command.ExpiryDate,
                command.CredentialUrl
            );

            await _portfolioRepository.UpdateAsync(portfolio, ct);

            var certification = portfolio.Certifications.First(c => c.Id.Value == command.CertificationId);
            return new CertificationDto(
                certification.Id,
                certification.Name,
                certification.IssuingOrganization,
                certification.IssueDate,
                certification.ExpiryDate,
                certification.CredentialUrl?.Value
            );
        }
    }
}
