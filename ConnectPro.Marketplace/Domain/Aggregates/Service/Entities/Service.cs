using ConnectPro.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain.Aggregates.Service.Entities
{
    public sealed class ServiceProjection : AggregateRoot<ServiceProjection>
    {
        public Guid ServiceId { get; private set; }
        public Guid FreelancerId { get; private set; }
        public string Title { get; private set; } = string.Empty;

        public bool IsActive { get; private set; }

        private ServiceProjection() { }

        public static ServiceProjection Create(Guid serviceId, Guid freelancerId, string title)
        {
            return new ServiceProjection
            {
                Id = Guid.NewGuid(),
                //ServiceId = ServiceId.Create(serviceId),
                FreelancerId = freelancerId,
                Title = title,
                IsActive = true
            };
        }

        public void Update(string title) => Title = title;

        public void Deactivate() => IsActive = false;
    }
}
