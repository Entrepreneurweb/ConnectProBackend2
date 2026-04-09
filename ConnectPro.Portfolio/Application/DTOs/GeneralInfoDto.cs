using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.DTOs
{
    public record GeneralInfoDto(
     string FirstName,
     string LastName,
     string? Bio,
     string? AvatarUrl
 );
}
