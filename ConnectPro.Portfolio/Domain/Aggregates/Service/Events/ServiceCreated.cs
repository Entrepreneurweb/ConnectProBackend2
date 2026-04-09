using ConnectPro.SharedKernel.Events;
using ConnectPro.SharedKernel.Events.Decorators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Aggregates.Service.Events
{

    [AggregateType("Service")]
    public class ServiceCreatedEvent : DomainEvent
    {
        public Guid ServiceId;
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ServiceCreatedEvent(Guid serviceId, Guid aggregateId, string name, string description)
            : base(aggregateId, DateTimeOffset.UtcNow)
        {   
            ServiceId = serviceId;
            Name = name;
            Description = description;
             
        }
    }
}
