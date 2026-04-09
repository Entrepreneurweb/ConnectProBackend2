using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Domain.Aggregates.Conversations.ValueObjects
{
    public record ParticipantSnapshot(Guid UserId, string DisplayName, string AvatarUrl);
}
