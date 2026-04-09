using ConnectPro.SharedKernel.Exceptions;
using MediatR;
using Portfolio.Application.Reviews.DTO;
using Portfolio.Domain.Aggregates.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Portfolio.Application.Reviews.Queries.GetReviewById
{
    public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, ReviewDto>
    {
        private readonly IReviewRepository _reviewRepository;

        public GetReviewByIdQueryHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<ReviewDto> Handle(GetReviewByIdQuery query, CancellationToken ct)
        {
            var review = await _reviewRepository.GetByIdAsync(query.ReviewId, ct);
            if (review is null)
                throw new DomainException("Review.NotFound", $"Review '{query.ReviewId}' was not found.");

            return new ReviewDto(
                review.Id,
                review.ServiceId,
                review.ReviewerId,
                review.Rating.Value,
                review.Comment,
                review.Status.ToString()
            );
        }
    }
}
