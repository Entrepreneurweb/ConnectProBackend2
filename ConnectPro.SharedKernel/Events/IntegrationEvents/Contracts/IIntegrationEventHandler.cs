using ConnectPro.SharedKernel.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Events.IntegrationEvents.Contracts
{
    
    public interface IIntegrationEventHandler<T> : INotificationHandler<T>
        where T : IIntegrationEvent
    {}
}
