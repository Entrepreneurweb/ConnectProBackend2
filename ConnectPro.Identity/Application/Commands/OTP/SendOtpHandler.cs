using ConnectPro.Identity.Domain;
using Identity.Domain.Aggregates.Enums;
using Identity.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Commands.OTP
{
    public class SendOtpHandler : IRequestHandler<SendOtpCommand>
    {
        private readonly IUserRepository _users;
        private readonly IUnitOfWork _unitOfWork;

        public SendOtpHandler(IUserRepository users , IUnitOfWork unitOfWork)
        {
            _users = users;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(SendOtpCommand request, CancellationToken cancellationToken)
        {
            var user = await _users.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null) {
                throw new Exception("User not found");
            }

            if (request.Action == OtpSendAction.SEND_OTP )
            {
                user.CreateOtp();
                
           
            }
            else if (request.Action == OtpSendAction.RESEND_OTP)
            {
                user.ReCreateOtp();
               
            }
            else
            {
                throw new Exception("Invalid OTP action");
            }

           // await _users.UpdateAsync(user, cancellationToken);
            var otpCode = user.GetPendingOtp()?.Code ?? throw new Exception("OTP generation failed");
            await  _users.SendOtpSenderService(user.GetEmail(), otpCode, cancellationToken);
            Console.WriteLine(user);

               await _users.UpdateAsync(user, cancellationToken);

        }
    }
}
