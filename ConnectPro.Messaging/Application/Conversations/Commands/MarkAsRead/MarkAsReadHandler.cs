using ConnectPro.SharedKernel;
using ConnectPro.SharedKernel.Exceptions;
using MediatR;
using Messaging.Domain.Aggregates.Conversations;
using Messaging.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Application.Conversations.Commands.MarkAsRead
{
    public class MarkAsReadHandler : IRequestHandler<MarkAsReadCommand>
    {
        private readonly IConversationRepository _repository;

        public MarkAsReadHandler(IConversationRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(MarkAsReadCommand command, CancellationToken ct)
        {
            var conversationId = Id<Conversation>.FromGuid(command.ConversationId);

            var conversation = await _repository.GetByIdAsync(conversationId, ct)
                ?? throw new DomainException("Conversation not found.");

            var messageId = Id<Message>.FromGuid(command.MessageId);
            var readerId = command.ReaderId;

            conversation.MarkAsRead(messageId, readerId);

            await _repository.UpdateAsync(conversation, ct);
        }
    }
}
