using ConnectPro.SharedKernel;
using Messaging.Domain.Aggregates.Conversations;
using Messaging.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Infrastructure.Persistence.Repositories
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly MessagingDbContext _context;

        public ConversationRepository(MessagingDbContext context)
        {
            _context = context;
        }

        public async Task<Conversation?> GetByIdAsync(Id<Conversation> id, CancellationToken ct = default)
        {
            return await _context.Conversations
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c => c.Id == id, ct);
        }

        public async Task<IEnumerable<Conversation>> GetByParticipantAsync(
            Guid userId,
            int page,
            int pageSize,
            CancellationToken ct = default)
        {
            return await _context.Conversations
                .Include(c => c.Messages)
                .Where(c => c.Participants.Any(p => p.UserId == userId))
                .OrderByDescending(c => c.LastMessageAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);
        }

        public async Task AddAsync(Conversation conversation, CancellationToken ct = default)
        {
            await _context.Conversations.AddAsync(conversation, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Conversation conversation, CancellationToken ct = default)
        {
            _context.Conversations.Update(conversation);
            await _context.SaveChangesAsync(ct);
        }
    }
}
