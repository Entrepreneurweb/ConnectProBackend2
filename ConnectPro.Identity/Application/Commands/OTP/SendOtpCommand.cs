using ConnectPro.SharedKernel;
using Identity.Domain.Aggregates.Enums;
using Identity.Domain.Aggregates.User.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Commands.OTP
{
    public record SendOtpCommand( Id<User> UserId,  OtpSendAction Action  ) :IRequest ;
}
