using MediatR;
using Portfolio.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Porfolio.Commands.ContactInfo
{
    public record UpdateContactInfoCommand(
     Guid PortfolioId,
     Guid RequestingUserId,
     string Email,
     string? Phone,
     string? Website
 ) : IRequest<ContactInfoDto>;
}
