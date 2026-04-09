using ConnectPro.SharedKernel.Exceptions;
using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.Application.Services.Commands.CreateService;
using Portfolio.Domain.Aggregates.Portfolio.Repository;
using Portfolio.Domain.Aggregates.Service.Repository;
using Portfolio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceEntity = Portfolio.Domain.Aggregates.Service.Entities.Service;

namespace Portfolio.Application.Services.Commands.UpdateService
{

    public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, ServiceDto>
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateServiceCommandHandler(
            IPortfolioRepository portfolioRepository,
            IServiceRepository serviceRepository,
            IUnitOfWork unitOfWork
            )
        {
            _portfolioRepository = portfolioRepository;
            _serviceRepository = serviceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceDto> Handle(UpdateServiceCommand command, CancellationToken ct)
        {
            var service = await _serviceRepository.GetByIdAsync(command.ServiceId, ct);
            if (service is null)
                throw new DomainException("service not found");

            var portfolio = await _portfolioRepository.GetByIdAsync(service.PortfolioId, ct);
            if (portfolio!.GetOwnerId != command.RequestingUserId)
                throw new DomainException( " user doesnot own the portfolio");

            service.UpdateContent(command.Title, command.Description);

            if (command.CoverImageUrl is not null)
                service.UpdateCoverImage(command.CoverImageUrl);

            await _serviceRepository.UpdateAsync(service, ct);
                await _unitOfWork.SaveChangesAsync(ct);

            return new ServiceDto(
                service.Id,
                service.PortfolioId,
                service.GetTitle,
                service.GetDescription,
                service.Status.ToString(),
                service.CoverImageUrl?.Value
            );
        }
    }

}
