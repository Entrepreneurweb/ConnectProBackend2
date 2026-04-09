using ConnectPro.SharedKernel;
using Marketplace.Domain.Aggregates.JobPost.Entities;
using Marketplace.Domain.Aggregates.JobPost.ValueObject;
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
    public sealed class JobPostConfiguration : IEntityTypeConfiguration<JobPost>
    {
        public void Configure(EntityTypeBuilder<JobPost> builder)
        {
            builder.ToTable("JobPosts");

            builder.HasKey(j => j.Id);

            builder.Property(j => j.Id)
                .HasConversion(id => id.Value , value => new Id<JobPost>(value) );

            builder.Property(j => j.ClientId)
                .IsRequired();

            builder.Property(j => j.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(j => j.Description)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(j => j.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(j => j.Budget)
                .HasColumnName("Budget")
                .IsRequired()
                .HasConversion(
                    b => b.Amount,
                    v => Budget.Create(v));

            builder.HasMany<ApplicationEntity>(j => j.Applications)
                .WithOne()
                .HasForeignKey("JobPostId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(j => j.Applications)
                .HasField("_applications")
                .AutoInclude();
        }
    }
}
