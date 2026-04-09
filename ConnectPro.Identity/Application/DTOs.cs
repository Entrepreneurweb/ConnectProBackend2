using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectPro.Identity.Application
{
   public record UserDto
    {
       
        public string? Email { get; }
        public Guid? Id { get; }
        public UserDto() { }
        public UserDto(  string Email , Guid Id)
        {
            
            this.Email = Email;
            this.Id = Id;
        }
    }
    public record AuthTokenDto( string Token);

}
