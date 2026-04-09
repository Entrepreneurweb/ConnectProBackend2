using ConnectPro.SharedKernel;
using Marketplace.Domain.Aggregates.JobPost.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationEntity = Marketplace.Domain.Aggregates.JobPost.Entities.Application;

namespace Marketplace.Infrastructure.Configurations
{
    public sealed class ApplicationConfiguration : IEntityTypeConfiguration<ApplicationEntity>
    {
        public void Configure(EntityTypeBuilder<ApplicationEntity> builder)
        {
            builder.ToTable("Applications");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasConversion(
                    v => v.Value,
                    v => new Id<ApplicationEntity>(v))
                .IsRequired();

            builder.Property(a => a.JobPostId)
                .HasConversion(
                    v => v.Value,
                    v => new Id<JobPost>(v))
                .IsRequired();

            builder.Property(a => a.FreelancerId)
                .IsRequired();

            builder.Property(a => a.CoverLetter)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(a => a.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.ComplexProperty(a => a.ProposedRate, r =>
            {
                r.Property(x => x.Amount)
                    .HasColumnName("ProposedRate")
                    .IsRequired();
            });
        }
    }
}
