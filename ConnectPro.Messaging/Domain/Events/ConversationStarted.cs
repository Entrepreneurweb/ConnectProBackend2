using ConnectPro.SharedKernel;
using ConnectPro.SharedKernel.Events;
using Messaging.Domain.Aggregates.Conversations;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConversationEntity = Messaging.Domain.Aggregates.Conversations.Conversation;



namespace Messaging.Domain.Events
{
    public record ConversationStarted(
    Id<Conversation> ConversationId,
    IEnumerable<Guid> ParticipantIds) : IDomainEvent
    {
        public Guid Id { get; } = Guid.NewGuid();
        public int Version { get; } = 1;
        public string AggregateType { get; } = nameof(Conversation);
        public string EventType { get; } = nameof(ConversationStarted);
        public Guid AggregateId { get; } = ConversationId.Value;
        public DateTimeOffset OccurredOnUtc { get; } = DateTimeOffset.UtcNow;
        public string? TraceInfo { get; } = null;
    }
}
