using ConnectPro.SharedKernel.Exceptions;
using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Aggregates.Service.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Services.Queries.GetServiceById
{
    public class GetServiceByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, ServiceFullDto>
    {
        private readonly IServiceRepository _serviceRepository;

        public GetServiceByIdQueryHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<ServiceFullDto> Handle(GetServiceByIdQuery query, CancellationToken ct)
        {
            var service = await _serviceRepository.GetByIdWithFullDetailsAsync(query.ServiceId, ct);
            if (service is null)
                throw new DomainException(" service not found");

            return new ServiceFullDto(
                Id: service.Id,
                PortfolioId: service.PortfolioId,
                Title: service.GetTitle,
                Description: service.GetDescription,
                Status: service.Status.ToString(),
                CoverImageUrl: service.CoverImageUrl?.Value,
                Pricing: service.Pricing is null ? null : new PricingDto(
                    service.Pricing.Amount,
                    service.Pricing.currency.ToString() ,
                    service.Pricing.Type.ToString()
                ),
                Tags: service.Tags
                    .Select(t => new TagDto(t.Id, t.Value))
                    .ToList(),
                Images: service.ImageUrls
                    .Select(i => new ImageUrlDto(i.Id, i.Value.Value))
                    .ToList(),
                Faqs: service.Faqs
                    .Select(f => new FaqDto(f.Id, f.Question, f.Answer))
                    .ToList(),
                Awards: service.Awards
                    .Select(a => new AwardDto(a.Id, a.Title, a.IssuedBy, a.IssuedAt, a.Description))
                    .ToList()
            );
        }
    }
}
