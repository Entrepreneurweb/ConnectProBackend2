using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Events.IntegrationEvents.Contracts
{
    public interface IIntegrationEvent : INotification
    {
        int Version { get; }
        string EventType { get; }
        Guid Id { get; }
        DateTimeOffset OccurredOnUtc { get; }
        Guid AggregateId { get; }
    }
}
