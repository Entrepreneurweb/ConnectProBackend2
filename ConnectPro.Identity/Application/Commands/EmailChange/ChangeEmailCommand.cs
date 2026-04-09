using ConnectPro.SharedKernel;
using Identity.Domain.Aggregates.User.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Commands.Email
{
    public record  ChangeEmailCommand (
        Id<User> UserId,
        string NewEmail
        ) : IRequest;
     }
