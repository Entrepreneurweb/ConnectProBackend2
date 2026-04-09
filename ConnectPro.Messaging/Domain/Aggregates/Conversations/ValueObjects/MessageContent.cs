using ConnectPro.SharedKernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Domain.Aggregates.Conversations.ValueObjects
{
    public record MessageContent
    {
        public string Text { get; }

        public MessageContent(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new DomainException("Message content cannot be empty.");
            if (text.Length > 2000)
                throw new DomainException("Message content exceeds 2000 characters.");

            Text = text.Trim();
        }

        public string Snippet() => Text.Length <= 80 ? Text : Text[..80] + "…";
    }
}
