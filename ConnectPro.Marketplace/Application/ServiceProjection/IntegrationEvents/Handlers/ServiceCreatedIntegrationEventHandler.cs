using SharedKernel.Events.IntegrationEvents.Contracts;
using SharedKernel.Events.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Application.ServiceProjection.IntegrationEvents.Handlers
{
    public class ServiceCreatedIntegrationEventHandler : IIntegrationEventHandler<ServiceCreatedIntegrationEvent>
    {
        public Task Handle(ServiceCreatedIntegrationEvent notification, CancellationToken cancellationToken)
        {
           Console.WriteLine($"Service created: {notification.Name} with ID: {notification.ServiceId}");
            return Task.CompletedTask;
        }
    }
}
