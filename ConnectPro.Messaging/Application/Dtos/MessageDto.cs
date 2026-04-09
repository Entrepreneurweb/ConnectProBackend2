using Messaging.Domain.Aggregates.Conversations.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Application.Dtos
{
    public record MessageDto(
   Guid Id,
   Guid SenderId,
   string Text,
   MessageStatus Status,
   DateTimeOffset SentAt,
   DateTime? ReadAt);
}
