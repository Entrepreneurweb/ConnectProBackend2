using ConnectPro.Identity.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailEntity = Identity.Domain.Aggregates.ValueObjects.Email;
namespace Identity.Application.Commands.Email
{
    public class ChangeEmailHandler : IRequestHandler<ChangeEmailCommand>
    {
        private readonly IUserRepository _userRepository;

        public ChangeEmailHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null) {
                throw new Exception("User not found");
            }else if (!user.IsActive())
            {
                throw new Exception("User is not active");
            }
            user.ChangeEmail( new  EmailEntity(request.NewEmail)  );
            throw new NotImplementedException();
        }
    }
}
