using ConnectPro.Identity.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Commands.SetActive
{
    public class SetActiveHandler : IRequestHandler<SetActiveCommand>
    {
        private readonly IUserRepository _userRepository;
            
        public SetActiveHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task Handle(SetActiveCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null) {
                
                throw new Exception("User not found");
            }

            var isActive = user.IsActive();
                if (isActive)
                {
                    user.Deactivate();
                }
                else
                {
                    user.Activate();
                }
    
                await _userRepository.UpdateAsync(user, cancellationToken);
            throw new NotImplementedException();
        }
    }
}
