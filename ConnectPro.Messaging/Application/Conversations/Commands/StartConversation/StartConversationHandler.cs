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

namespace Messaging.Application.Conversations.Commands.StartConversation
{
    public class StartConversationHandler : IRequestHandler<StartConversationCommand, Id<Conversation>>
    {
        private readonly IConversationRepository _repository;
        private readonly IUserExistenceChecker _userChecker;

        public StartConversationHandler(
            IConversationRepository repository,
            IUserExistenceChecker userChecker)
        {
            _repository = repository;
            _userChecker = userChecker;
        }

        public async Task<Id<Conversation>> Handle(StartConversationCommand command, CancellationToken ct)
        {
            var initiatorId = command.InitiatorId;
            var recipientId = command.RecipientId;

            if (!await _userChecker.ExistsAsync(initiatorId, ct))
                throw new DomainException("Initiator does not exist.");

            if (!await _userChecker.ExistsAsync(recipientId, ct))
                throw new DomainException("Recipient does not exist.");

            // TODO : récupérer les snapshots via IUserSnapshotProvider (ACL vers Identity BC)
            var participants = new List<ParticipantSnapshot>
        {
            new(initiatorId, "placeholder", "placeholder"),
            new(recipientId, "placeholder", "placeholder")
        };

            var conversation = new Conversation(participants);

            await _repository.AddAsync(conversation, ct);

            return conversation.Id;
        }
    }
}
