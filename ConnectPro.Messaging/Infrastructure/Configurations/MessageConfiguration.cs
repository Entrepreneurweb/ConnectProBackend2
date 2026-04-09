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
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .HasConversion(id => id.Value, value => Id<Message>.FromGuid(value));

            builder.Property(m => m.SenderId);

            builder.Property(m => m.Status)
                .HasConversion<string>();

            builder.Property(m => m.ReadAt);

            builder.OwnsOne(m => m.Content, content =>
            {
                content.Property(c => c.Text)
                    .HasColumnName("Content")
                    .HasMaxLength(2000);
            });
        }
    }
}
