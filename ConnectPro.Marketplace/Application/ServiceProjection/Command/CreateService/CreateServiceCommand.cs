using Marketplace.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Application.ServiceProjection.Command.CreateService
{
    public class CreateServiceCommand  : IRequest<ServiceProjectionDto>
    {
        public Guid ServiceId;
        public Guid PortfolioId { get; }
        public Guid RequestingUserId { get; }
        public string Title { get; }
        public string Description { get; }
        public CreateServiceCommand(Guid ServiceId, Guid portfolioId, Guid requestingUserId, string title, string description)
        {   
            this.ServiceId = ServiceId;
            PortfolioId = portfolioId;
            RequestingUserId = requestingUserId;
            Title = title;
            Description = description;
        }

    }
}
