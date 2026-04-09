using ConnectPro.SharedKernel;
using Messaging.Domain.Aggregates.Conversations.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Domain.Aggregates.Conversations
{
    public class Message : Entity<Message>
    {
        public Guid SenderId { get; private set; }
        public MessageContent Content { get; private set; }
        public MessageStatus Status { get; private set; }
        public DateTime? ReadAt { get; private set; }

        private Message() { }

        public Message(Guid senderId, MessageContent content) : base()
        {
            SenderId = senderId;
            Content = content;
            Status = MessageStatus.Sent;
        }

        public void MarkAsRead(Guid readerId)
        {
            if (SenderId == readerId) return;
            if (Status == MessageStatus.Read) return;

            Status = MessageStatus.Read;
            ReadAt = DateTime.UtcNow;
        }
    }
}
