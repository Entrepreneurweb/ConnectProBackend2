using Marketplace.Domain.Aggregates.Service.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Application.ServiceProjection.ServiceCreated
{
    /*
    public sealed class ServiceCreatedHandler : INotificationHandler<ServiceCreated>
    {
        private readonly IServiceProjectionRepository _repository;

        public ServiceCreatedHandler(IServiceProjectionRepository repository)
            => _repository = repository;

        public async Task Handle(ServiceCreated notification, CancellationToken cancellationToken)
        {
            var projection = Domain.Aggregates.ServiceProjection.Create(
                notification.ServiceId,
                notification.FreelancerId,
                notification.Title);

            await _repository.AddAsync(projection, cancellationToken);
        }
    }

    */
}
