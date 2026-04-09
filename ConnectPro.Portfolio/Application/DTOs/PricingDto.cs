using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.DTOs
{
    public record PricingDto(
     decimal Amount,
     string Currency,
     string Type
 );

}
