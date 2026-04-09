using MediatR;
using Messaging.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Application.Conversations.Queries.GetConversation
{
    public record GetConversationQuery(Guid ConversationId, Guid RequesterId) : IRequest<ConversationDto>;
}
