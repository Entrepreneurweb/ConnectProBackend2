using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Application.Conversations.Commands.MarkAsRead
{
    public record MarkAsReadCommand(
    Guid ConversationId,
    Guid MessageId,
    Guid ReaderId) : IRequest;
}
