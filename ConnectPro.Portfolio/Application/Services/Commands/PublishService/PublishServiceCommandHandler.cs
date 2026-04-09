using ConnectPro.SharedKernel.Exceptions;
//using Identity.Infrastructure;
using MediatR;
using Portfolio.Domain.Aggregates.Portfolio.Repository;
using Portfolio.Domain.Aggregates.Service.Repository;
using Portfolio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Services.Commands.PublishService
{
    public class PublishServiceCommandHandler : IRequestHandler<PublishServiceCommand>
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PublishServiceCommandHandler(
            IPortfolioRepository portfolioRepository,
            IServiceRepository serviceRepository ,
            IUnitOfWork unitOfWork)
        {
            _portfolioRepository = portfolioRepository;
            _serviceRepository = serviceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(PublishServiceCommand command, CancellationToken ct)
        {
            var service = await _serviceRepository.GetByIdWithFullDetailsAsync(command.ServiceId, ct);
            if (service is null)
                throw new DomainException(" service not found");

            var portfolio = await _portfolioRepository.GetByIdAsync(service.PortfolioId, ct);
            if (portfolio!.GetOwnerId != command.RequestingUserId)
                throw new DomainException( " user doesnot own the portfolio");

            service.Publish();
            await _serviceRepository.UpdateAsync(service, ct);
             await _unitOfWork.SaveChangesAsync() ;
            //SaveChangesAsync
        }
    }
}
