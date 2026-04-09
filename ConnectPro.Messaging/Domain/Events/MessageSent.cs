using ConnectPro.SharedKernel;
using ConnectPro.SharedKernel.Events;
using Messaging.Domain.Aggregates.Conversations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Domain.Events
{
    public record MessageSent(
     Id<Conversation> ConversationId,
     Id<Message> MessageId,
     Guid SenderId,
     string Snippet) : IDomainEvent
    {
        public Guid Id { get; } = Guid.NewGuid();
        public int Version { get; } = 1;
        public string AggregateType { get; } = nameof(Conversation);
        public string EventType { get; } = nameof(MessageSent);
        public Guid AggregateId { get; } = ConversationId.Value;
        public DateTimeOffset OccurredOnUtc { get; } = DateTimeOffset.UtcNow;
        public string? TraceInfo { get; } = null;
    }

}
