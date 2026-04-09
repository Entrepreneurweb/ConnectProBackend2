using ConnectPro.SharedKernel;
using ConnectPro.SharedKernel.Exceptions;
using MediatR;
using Messaging.Application.Abstractions;
using Messaging.Domain.Aggregates.Conversations;
using Messaging.Domain.Aggregates.Conversations.ValueObjects;
using Messaging.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Application.Conversations.Commands.SendMessage
{
    public class SendMessageHandler : IRequestHandler<SendMessageCommand>
    {
        private readonly IConversationRepository _repository;
        private readonly IUserExistenceChecker _userChecker;

        public SendMessageHandler(
            IConversationRepository repository,
            IUserExistenceChecker userChecker)
        {
            _repository = repository;
            _userChecker = userChecker;
        }

        public async Task Handle(SendMessageCommand command, CancellationToken ct)
        {
            var senderId = command.SenderId;

            if (!await _userChecker.ExistsAsync(senderId, ct))
                throw new DomainException("Sender does not exist.");

            var conversationId = Id<Conversation>.FromGuid(command.ConversationId);

            var conversation = await _repository.GetByIdAsync(conversationId, ct)
                ?? throw new DomainException("Conversation not found.");

            var content = new MessageContent(command.Text);

            conversation.SendMessage(senderId, content);

            await _repository.UpdateAsync(conversation, ct);
        }
    }
}
