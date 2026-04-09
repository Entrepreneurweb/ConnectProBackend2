using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Reviews.DTO
{
    public record ReviewDto(
     Guid Id,
     Guid ServiceId,
     Guid ReviewerId,
     int Rating,
     string Comment,
     string Status
 );
}
