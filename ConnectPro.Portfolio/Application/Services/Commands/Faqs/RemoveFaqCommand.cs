using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Services.Commands.Faqs
{
    public record RemoveFaqCommand(
     Guid ServiceId,
     Guid RequestingUserId,
     Guid FaqId
 ) : IRequest;
}
