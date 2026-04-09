using Marketplace.Domain.Aggregates.Service.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Application.ServiceProjection.ServiceUpdated
{
    /*
    public sealed class ServiceUpdatedHandler : INotificationHandler<ServiceUpdated>
    {
        private readonly IServiceProjectionRepository _repository;

        public ServiceUpdatedHandler(IServiceProjectionRepository repository)
            => _repository = repository;

        public async Task Handle(ServiceUpdated notification, CancellationToken cancellationToken)
        {
            var projection = await _repository.GetByServiceIdAsync(notification.ServiceId, cancellationToken);

            if (projection is null) return;

            projection.Update(notification.Title);

            await _repository.UpdateAsync(projection, cancellationToken);
        }
    }
    */

}
