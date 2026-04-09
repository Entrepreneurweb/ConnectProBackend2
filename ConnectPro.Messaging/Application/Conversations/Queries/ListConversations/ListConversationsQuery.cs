using MediatR;
using Messaging.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Application.Conversations.Queries.ListConversations
{
    public record ListConversationsQuery(Guid UserId, int Page = 1, int PageSize = 20) : IRequest<IEnumerable<ConversationDto>>;
}
