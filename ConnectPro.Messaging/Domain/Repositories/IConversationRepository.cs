using ConnectPro.SharedKernel;
using Messaging.Domain.Aggregates.Conversations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Domain.Repositories
{
    public interface IConversationRepository
    {
        Task<Conversation?> GetByIdAsync(Id<Conversation> id, CancellationToken ct = default);
        Task AddAsync(Conversation conversation, CancellationToken ct = default);
        Task UpdateAsync(Conversation conversation, CancellationToken ct = default);
        Task<IEnumerable<Conversation>> GetByParticipantAsync(Guid userId, int page, int pageSize, CancellationToken ct);
    }
}
