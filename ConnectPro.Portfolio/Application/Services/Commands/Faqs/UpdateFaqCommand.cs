using MediatR;
using Portfolio.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Services.Commands.Faqs
{
    public record UpdateFaqCommand(
     Guid ServiceId,
     Guid RequestingUserId,
     Guid FaqId,
     string Question,
     string Answer
 ) : IRequest<FaqDto>;

}
