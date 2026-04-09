using ConnectPro.SharedKernel.Exceptions;
using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Aggregates.Portfolio.Repository;
using Portfolio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using PortfolioEntity = Portfolio.Domain.Aggregates.Portfolio.Entities.Portfolio;

namespace Portfolio.Application.Porfolio.Commands.CreatePortfolio
{

    public class CreatePortfolioCommandHandler : IRequestHandler<CreatePortfolioCommand, PortfolioDto>
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public CreatePortfolioCommandHandler(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task<PortfolioDto> Handle(CreatePortfolioCommand command, CancellationToken ct)
        {
            var existing = await _portfolioRepository.GetByOwnerIdAsync(command.OwnerId, ct);
            if (existing is not null)
                throw new DomainException( " Already exist");

            if (!Enum.TryParse<PortfolioType>(command.Type, ignoreCase: true, out var portfolioType))
                throw new DomainException("Portfolio.InvalidType", $"Invalid portfolio type '{command.Type}'.");

            var portfolio = PortfolioEntity.Create(command.OwnerId , portfolioType);
            await _portfolioRepository.AddAsync(portfolio, ct);

            //return new PortfolioDto(
            //    portfolio.Id,
            //    portfolio.GetOwnerId,
            //    portfolio.GetPortfolioType,
            //    portfolio.Status.ToString(),
            //    portfolio.ActiveServicesCount
            //);
            return new PortfolioDto(portfolio.Id.Value, portfolio.GetOwnerId, $" {portfolio.GetType} "   , $" {portfolio.Status} " , portfolio.ActiveServicesCount);
            
        }
    }
}
