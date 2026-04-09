using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Application.Conversations.Commands.SendMessage
{
    public record SendMessageCommand(
     Guid ConversationId,
     Guid SenderId,
     string Text) : IRequest;
}
