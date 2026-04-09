using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Application.Dtos
{
    public record ParticipantDto(
     Guid UserId,
     string DisplayName,
     string AvatarUrl);
}
