using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Application.Dtos
{
    public sealed record JobPostDto(
     Guid Id,
     Guid ClientId,
     string Title,
     string Description,
     decimal Budget,
     string Status,
     IReadOnlyCollection<ApplicationDto> Applications
 );

    public sealed record ApplicationDto(
        Guid Id,
        Guid FreelancerId,
        string CoverLetter,
        decimal ProposedRate,
        string Status
    );

}
