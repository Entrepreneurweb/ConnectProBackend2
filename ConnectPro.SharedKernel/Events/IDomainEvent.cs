using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectPro.SharedKernel.Events
{
    public interface IDomainEvent : INotification
    {
        int Version { get; }

        string AggregateType { get; }

        string EventType { get; }

        Guid Id { get; }

        DateTimeOffset OccurredOnUtc { get; }

        Guid AggregateId { get; }

        string? TraceInfo { get; }
    }
}
