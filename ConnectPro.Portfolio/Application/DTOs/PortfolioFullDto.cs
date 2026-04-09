using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.DTOs
{
    public record PortfolioFullDto(
     Guid Id,
     Guid OwnerId,
     string Type,
     string Status,
     int ActiveServicesCount,
     GeneralInfoDto? GeneralInfo,
     ContactInfoDto? ContactInfo,
     LocationInfoDto? LocationInfo,
     ProfessionalInfoDto? ProfessionalInfo,
     IReadOnlyList<ExperienceDto> Experiences,
     IReadOnlyList<CertificationDto> Certifications
 );
}
