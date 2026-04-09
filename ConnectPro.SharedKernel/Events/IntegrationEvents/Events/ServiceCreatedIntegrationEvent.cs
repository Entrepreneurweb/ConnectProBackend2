using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Events.IntegrationEvents.Events
{
    public class ServiceCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid ServiceId { get; set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public ServiceCreatedIntegrationEvent( Guid id , Guid aggregateId, string name, string description)
            : base(aggregateId)
        {
            Id = id;
            Name = name;
            Description = description;

        }

    }
}
