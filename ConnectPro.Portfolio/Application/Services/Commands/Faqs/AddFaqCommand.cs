using MediatR;
using Portfolio.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Services.Commands.Faqs
{
    public record AddFaqCommand(
     Guid ServiceId,
     Guid RequestingUserId,
     string Question,
     string Answer
 ) : IRequest<FaqDto>;

}
