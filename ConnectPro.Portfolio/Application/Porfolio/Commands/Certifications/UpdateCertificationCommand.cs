using MediatR;
using Portfolio.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Porfolio.Commands.Certifications
{
    public record UpdateCertificationCommand(
   Guid PortfolioId,
   Guid RequestingUserId,
   Guid CertificationId,
   string Name,
   string IssuingOrganization,
   DateOnly IssueDate,
   DateOnly? ExpiryDate,
   string? CredentialUrl
) : IRequest<CertificationDto>;

}
