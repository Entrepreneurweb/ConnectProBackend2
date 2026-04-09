using ConnectPro.SharedKernel.Events;
using MediatR;
using Portfolio.Domain.Aggregates.Service.Events;
using SharedKernel.Events.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Services.EventHandler
{
    public class ServiceCreatedEventHandler : IDomainEventHandler<ServiceCreatedEvent>
    {
        private readonly IPublisher _publisher;

        public ServiceCreatedEventHandler(IPublisher publisher)
        {
            _publisher = publisher;
        }
        public Task Handle(ServiceCreatedEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Service created: {notification.Name} - {notification.Description}");

            var integrationEvent = new ServiceCreatedIntegrationEvent( notification.ServiceId,   notification.AggregateId , notification.Name , notification.Description );
            _publisher.Publish(integrationEvent, cancellationToken);
            return Task.CompletedTask;
        }
    }
}
