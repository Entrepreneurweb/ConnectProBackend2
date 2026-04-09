using ConnectPro.SharedKernel;
using Messaging.Domain.Aggregates.Conversations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Infrastructure.Configurations
{
    public class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasConversion(id => id.Value, value => Id<Conversation>.FromGuid(value));

            builder.Property(c => c.LastMessageAt);

            builder.OwnsMany(c => c.Participants, participant =>
            {
                participant.WithOwner().HasForeignKey("ConversationId");
                participant.Property(p => p.UserId);
                     
                participant.Property(p => p.DisplayName);
                participant.Property(p => p.AvatarUrl);
            });

            builder.HasMany(c => c.Messages)
                .WithOne()
                .HasForeignKey("ConversationId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(c => c.DomainEvents);
        }
    }
}
