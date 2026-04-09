using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.DTOs
{
    public record ServiceDto(
     Guid Id,
     Guid PortfolioId,
     string Title,
     string Description
,
     string V
,
     string? value
 //string Status
 );
}
