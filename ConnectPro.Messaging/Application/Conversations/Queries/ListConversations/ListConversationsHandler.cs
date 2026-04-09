using ConnectPro.SharedKernel;
using MediatR;
using Messaging.Application.Dtos;
using Messaging.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Application.Conversations.Queries.ListConversations
{
    public class ListConversationsHandler : IRequestHandler<ListConversationsQuery, IEnumerable<ConversationDto>>
    {
        private readonly IConversationRepository _repository;

        public ListConversationsHandler(IConversationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ConversationDto>> Handle(ListConversationsQuery query, CancellationToken ct)
        {
            var userId = query.UserId;

            var conversations = await _repository.GetByParticipantAsync(userId, query.Page, query.PageSize, ct);

            return conversations.Select(c => new ConversationDto(
                c.Id.Value,
                c.Participants.Select(p => new ParticipantDto(p.UserId, p.DisplayName, p.AvatarUrl)),
                c.Messages.Select(m => new MessageDto(m.Id.Value, m.SenderId, m.Content.Text, m.Status, m.CreatedAtUtc, m.ReadAt)),
                c.LastMessageAt));
        }
    }
}
