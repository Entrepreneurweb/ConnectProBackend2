using ConnectPro.Identity.Domain;
using ConnectPro.Identity.Domain.Aggregates.ValueObjects;
using ConnectPro.SharedKernel.Exceptions;
using Identity.Domain.Aggregates.User.Entities;
using Identity.Domain.Aggregates.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectPro.Identity.Application.Commands.RegisterUser
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, UserDto>
    {
        private readonly IUserRepository _users;
      //  private readonly IPasswordHasher _hasher;

        public RegisterUserHandler(IUserRepository users  /*, IPasswordHasher hasher */ )
        {
            _users = users;
          //  _hasher = hasher;
        }

        public async Task<UserDto> Handle(RegisterUserCommand cmd, CancellationToken ct)
        {
            var email = new Email(cmd.Email);
            
            if (await _users.ExistsByEmailAsync(email, ct))
                throw new DomainException(cmd.Email);

           var passwordHash =  new PasswordHash(cmd.Password);
              var user = User.Create(email, passwordHash );
 

            await _users.AddAsync(user, ct);

            return new UserDto(  user.GetEmail().Value , user.Id.Value ) ;
        }
    }
}
