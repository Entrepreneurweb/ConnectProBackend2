using ConnectPro.SharedKernel;
using Identity.Domain.Aggregates.User.Entities;
using Identity.Domain.Aggregates.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Commands.VerifyOtp
{
    public record VerifyOtpCommand(
        Id<User> UserId,
        string OtpCode
        ) : IRequest<bool>;

}
