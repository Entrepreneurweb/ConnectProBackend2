using ConnectPro.SharedKernel;
using MediatR;
using Messaging.Domain.Aggregates.Conversations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Application.Conversations.Commands.StartConversation
{
    public record StartConversationCommand(
     Guid InitiatorId,
     Guid RecipientId) : IRequest<Id<Conversation>>;
}
