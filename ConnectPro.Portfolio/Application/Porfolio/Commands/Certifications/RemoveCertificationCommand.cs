using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Porfolio.Commands.Certifications
{
    public record RemoveCertificationCommand(
    Guid PortfolioId,
    Guid RequestingUserId,
    Guid CertificationId
) : IRequest;

}
