using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.DTOs
{
    public record ExperienceDto(
     Guid Id,
     string Company,
     string Role,
     string? Description,
     DateOnly Start,
     DateOnly? End
 );
}
