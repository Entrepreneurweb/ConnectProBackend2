using ConnectPro.Identity.Domain;
using ConnectPro.Identity.Domain.Aggregates.ValueObjects;
using ConnectPro.SharedKernel.Exceptions;
using Identity.Domain.Aggregates.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace ConnectPro.Identity.Application.Commands.LoginUser
{

    public class LoginUserHandler : IRequestHandler<LoginUserCommand, AuthTokenDto>
    {
        private readonly IUserRepository _users;
       // private readonly IPasswordHasher _hasher;
        private readonly ITokenService _tokenService;

        public LoginUserHandler(
            IUserRepository users,
         //   IPasswordHasher hasher,
          ITokenService tokenService
            )
        {
            _users = users;
          //  _hasher = hasher;
           _tokenService = tokenService;
        }

        public async Task<AuthTokenDto> Handle(LoginUserCommand cmd, CancellationToken ct)
        {
            var email = new Email(cmd.Email);
            var user = await _users.GetByEmailAsync(email, ct)
                        ?? throw new DomainException("Invalid Credentials");

            if( user.GetPendingOtp() != null)
            {
              var partialToken = _tokenService.GeneratePartialToken(user);
                return new AuthTokenDto(partialToken);
            }
            if (!user.IsActive() )
                throw new DomainException("Iser is not active");

            //if (!_hasher.Verify(cmd.Password, user.PasswordHash))
            //    throw new InvalidCredentialsException();

            var token = _tokenService.GenerateToken(user);

            return new AuthTokenDto( token);
        }
    }
}
