using ConnectPro.Identity.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Commands.ProfilePicture
{
    public class SetProfilePictureHandler : IRequestHandler<SetProfilePictureCommand>
    {
        private readonly IUserRepository _userRepository;
        
        public SetProfilePictureHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task Handle(SetProfilePictureCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }else if (!user.IsActive())
            {
                throw new Exception("User is not active");
            }
                user.ChangeProfilePicture(request.PictureUrl);
                await _userRepository.UpdateAsync(user , cancellationToken);

           
        }
    }
}
