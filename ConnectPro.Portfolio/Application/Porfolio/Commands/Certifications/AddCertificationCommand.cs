using MediatR;
using Portfolio.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Porfolio.Commands.Certifications
{

    public record AddCertificationCommand(
        Guid PortfolioId,
        Guid RequestingUserId,
        string Name,
        string IssuingOrganization,
        DateOnly IssueDate,
        DateOnly? ExpiryDate,
        string? CredentialUrl
    ) : IRequest<CertificationDto>;

}
