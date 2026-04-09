using ConnectPro.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Aggregates.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Infrastructure.Persistence.Configurations
{
    public class FaqConfiguration : IEntityTypeConfiguration<FAQ>
    {
        public void Configure(EntityTypeBuilder<FAQ> builder)
        {
            builder.ToTable("Service_Faqs");

            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id)
                .HasConversion(
                    id => id.Value,
                    value => new Id<FAQ>(value))
                .ValueGeneratedNever();
            builder.Property(f => f.ServiceId).IsRequired();

            builder.Property<string>("_question")
                .HasColumnName("Question")
                .HasMaxLength(300)
                .IsRequired();

            builder.Property<string>("_answer")
                .HasColumnName("Answer")
                .HasMaxLength(2000)
                .IsRequired();

            builder.HasIndex(f => f.ServiceId)
                .HasDatabaseName("IX_Faqs_ServiceId");
        }
    }
}
