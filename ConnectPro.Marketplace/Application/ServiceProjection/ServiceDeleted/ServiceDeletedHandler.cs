using Marketplace.Domain.Aggregates.Service.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Application.ServiceProjection.ServiceDeleted
{
    /*
    public sealed class ServiceDeletedHandler : INotificationHandler<ServiceDeleted>
    {
        private readonly IServiceProjectionRepository _repository;

        public ServiceDeletedHandler(IServiceProjectionRepository repository)
            => _repository = repository;

        public async Task Handle(ServiceDeleted notification, CancellationToken cancellationToken)
        {
            var projection = await _repository.GetByServiceIdAsync(notification.ServiceId, cancellationToken);

            if (projection is null) return;

            projection.Deactivate();

            await _repository.UpdateAsync(projection, cancellationToken);
        }
    }
    */
}
