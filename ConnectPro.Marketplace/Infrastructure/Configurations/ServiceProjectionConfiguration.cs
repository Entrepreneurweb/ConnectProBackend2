using ConnectPro.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceProjection = Marketplace.Domain.Aggregates.Service.Entities.ServiceProjection;
namespace Marketplace.Infrastructure.Configurations
{
    public sealed class ServiceProjectionConfiguration : IEntityTypeConfiguration<ServiceProjection>
    {
        public void Configure(EntityTypeBuilder<ServiceProjection> builder)
        {
            builder.ToTable("ServiceProjections");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .HasConversion( id => id.Value, value => new Id<ServiceProjection>(value) );

            builder.Property(s => s.FreelancerId)
                .IsRequired();

            builder.Property(s => s.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(s => s.IsActive)
                .IsRequired();

            builder.Property(s => s.ServiceId)
                .HasColumnName("ServiceId")
                .IsRequired();
                /*.HasConversion(
                    sid => sid,
                    v => ServiceId.Create(v));*/

            builder.HasIndex("ServiceId")
                .IsUnique();
        }
    }

}
