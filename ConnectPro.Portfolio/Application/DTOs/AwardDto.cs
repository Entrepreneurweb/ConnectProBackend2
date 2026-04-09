using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.DTOs
{
    public record AwardDto(
     Guid Id,
     string Title,
     string IssuedBy,
     DateOnly IssuedAt,
     string? Description
 );
}
