using Marketplace.Application.Dtos;
using Marketplace.Domain.Aggregates.Service.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Application.ServiceProjection.Command.CreateService
{
    public sealed class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, ServiceProjectionDto>
    {
        private readonly IServiceProjectionRepository _serviceProjectionRepository;

        public CreateServiceCommandHandler(IServiceProjectionRepository serviceProjectionRepository)
        {
            _serviceProjectionRepository = serviceProjectionRepository;
        }

        public Task<ServiceProjectionDto> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {

            var existingServiceProjection = _serviceProjectionRepository.GetByServiceIdAsync(request.ServiceId);
            if (existingServiceProjection != null)
                {
                throw new InvalidOperationException($"A service projection with ID {request.ServiceId} already exists.");
            }



            var serviceProjection =  Servi //_serviceProjectionRepository.Create(request.ServiceId, request.PortfolioId, request.RequestingUserId, request.Title, request.Description);
            throw new NotImplementedException();
        }
    }
}
