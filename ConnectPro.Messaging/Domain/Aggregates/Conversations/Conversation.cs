using ConnectPro.SharedKernel;
using ConnectPro.SharedKernel.Exceptions;
using Messaging.Domain.Aggregates.Conversations.ValueObjects;
using Messaging.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Domain.Aggregates.Conversations
{
    public class Conversation : AggregateRoot<Conversation>
    {
        private readonly List<Message> _messages = new();
        private readonly List<ParticipantSnapshot> _participants = new();

        public DateTime? LastMessageAt { get; private set; }

        public IReadOnlyCollection<Message> Messages => _messages.AsReadOnly();
        public IReadOnlyCollection<ParticipantSnapshot> Participants => _participants.AsReadOnly();

        private Conversation() { }

        public Conversation(IEnumerable<ParticipantSnapshot> participants) : base()
        {
            var participantList = participants.ToList();

            if (participantList.Count < 2)
                throw new DomainException("A conversation requires at least 2 participants.");

            _participants.AddRange(participantList);

            RaiseDomainEvent(new ConversationStarted(Id, _participants.Select(p => p.UserId)));
        }

        public void SendMessage(Guid senderId, MessageContent content)
        {
            EnsureParticipant(senderId);

            var message = new Message(senderId, content);
            _messages.Add(message);
            LastMessageAt = DateTime.UtcNow;

            RaiseDomainEvent(new MessageSent(Id, message.Id, senderId, content.Snippet()));
        }

        public void MarkAsRead(Id<Message> messageId, Guid readerId)
        {
            EnsureParticipant(readerId);

            var message = _messages.FirstOrDefault(m => m.Id == messageId)
                ?? throw new DomainException("Message not found.");

            message.MarkAsRead(readerId);

            RaiseDomainEvent(new MessageRead(Id, messageId, readerId, DateTime.UtcNow));
        }

        private void EnsureParticipant(Guid userId)
        {
            if (_participants.All(p => p.UserId != userId))
                throw new DomainException("User is not a participant of this conversation.");
        }
    }
}
