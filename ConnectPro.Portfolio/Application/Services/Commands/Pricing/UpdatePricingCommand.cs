using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Portfolio.Domain.Aggregates.Service.ValueObject.Pricing;

namespace Portfolio.Application.Services.Commands.Pricing
{
    public record UpdatePricingCommand(
     Guid ServiceId,
     Guid RequestingUserId,
     decimal Amount,
     Currency Currency,
     PricingType Type
 ) : IRequest<PricingDto>;
}
