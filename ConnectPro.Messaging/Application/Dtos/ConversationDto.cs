using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Application.Dtos
{
    public record ConversationDto(
    Guid Id,
    IEnumerable<ParticipantDto> Participants,
    IEnumerable<MessageDto> Messages,
    DateTime? LastMessageAt);
}
