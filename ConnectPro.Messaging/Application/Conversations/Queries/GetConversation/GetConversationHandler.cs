using ConnectPro.SharedKernel;
using ConnectPro.SharedKernel.Exceptions;
using MediatR;
using Messaging.Application.Dtos;
using Messaging.Domain.Aggregates.Conversations;
using Messaging.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Application.Conversations.Queries.GetConversation
{
    public class GetConversationHandler : IRequestHandler<GetConversationQuery, ConversationDto>
    {
        private readonly IConversationRepository _repository;

        public GetConversationHandler(IConversationRepository repository)
        {
            _repository = repository;
        }

        public async Task<ConversationDto> Handle(GetConversationQuery query, CancellationToken ct)
        {
            var conversationId = Id<Conversation>.FromGuid(query.ConversationId);

            var conversation = await _repository.GetByIdAsync(conversationId, ct)
                ?? throw new DomainException("Conversation not found.");

            var requesterId = query.RequesterId;

            if (conversation.Participants.All(p => p.UserId != requesterId))
                throw new DomainException("Access denied.");

            return new ConversationDto(
                conversation.Id.Value,
                conversation.Participants.Select(p => new ParticipantDto(p.UserId, p.DisplayName, p.AvatarUrl)),
                conversation.Messages.Select(m => new MessageDto(m.Id.Value, m.SenderId, m.Content.Text, m.Status, m.CreatedAtUtc, m.ReadAt)),
                conversation.LastMessageAt);
        }
    }
}
