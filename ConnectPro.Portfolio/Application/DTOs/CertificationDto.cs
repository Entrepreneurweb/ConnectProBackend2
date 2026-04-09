using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.DTOs
{
    public record CertificationDto(
     Guid Id,
     string Name,
     string IssuingOrganization,
     DateOnly IssueDate,
     DateOnly? ExpiryDate,
     string? CredentialUrl
 );
}
