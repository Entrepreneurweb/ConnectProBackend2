using ConnectPro.Identity.Domain;
using Identity.Domain.Aggregates.ValueObjects;
using Identity.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Commands.VerifyOtp
{
    public class VerifyOtpHandler : IRequestHandler<VerifyOtpCommand, bool>
    {
        private readonly IUserRepository _users;
        private readonly IUnitOfWork _unitOfWork;

        public VerifyOtpHandler(IUserRepository users  , IUnitOfWork unitOfWork)
        {
            _users = users;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
        {
           // var user =  await _users.GetByIdAsync(request.UserId, cancellationToken);
            var user = await _users.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            if (user.GetPendingOtp() == null)
            {
                throw new Exception("No pending OTP for this user");
            }

            var otp = new OtpCode(request.OtpCode);
            user.VerifyOtp(otp);
                
               // await _users.UpdateAsync(user, cancellationToken);
              await _users.UpdateAsync(user , cancellationToken) ;
            return true;

             
        }
    }
}
