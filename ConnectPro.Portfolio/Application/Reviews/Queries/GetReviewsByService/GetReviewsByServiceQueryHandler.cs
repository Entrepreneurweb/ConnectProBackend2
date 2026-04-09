using ConnectPro.SharedKernel.Exceptions;
using MediatR;
using Portfolio.Application.Reviews.DTO;
using Portfolio.Domain.Aggregates.Review;
using Portfolio.Domain.Aggregates.Service.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Reviews.Queries.GetReviewsByService
{
    public class GetReviewsByServiceQueryHandler : IRequestHandler<GetReviewsByServiceQuery, IReadOnlyList<ReviewDto>>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IReviewRepository _reviewRepository;

        public GetReviewsByServiceQueryHandler(
            IServiceRepository serviceRepository,
            IReviewRepository reviewRepository)
        {
            _serviceRepository = serviceRepository;
            _reviewRepository = reviewRepository;
        }

        public async Task<IReadOnlyList<ReviewDto>> Handle(GetReviewsByServiceQuery query, CancellationToken ct)
        {
            var service = await _serviceRepository.GetByIdAsync(query.ServiceId, ct);
            if (service is null)
                throw new DomainException("Service not found");

            var reviews = await _reviewRepository.GetByServiceIdAsync(query.ServiceId, ct);

            return reviews
                .Select(r => new ReviewDto(
                    r.Id,
                    r.ServiceId,
                    r.ReviewerId,
                    r.Rating.Value,
                    r.Comment,
                    r.Status.ToString()
                ))
                .ToList();
        }
    }

}
