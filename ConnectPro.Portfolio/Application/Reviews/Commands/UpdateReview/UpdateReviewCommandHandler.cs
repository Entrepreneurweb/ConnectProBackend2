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

namespace Portfolio.Application.Reviews.Commands.UpdateReview
{
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, ReviewDto>
    {
        private readonly IReviewRepository _reviewRepository;

        public UpdateReviewCommandHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<ReviewDto> Handle(UpdateReviewCommand command, CancellationToken ct)
        {
            var review = await _reviewRepository.GetByIdAsync(command.ReviewId, ct);
            if (review is null)
                throw new DomainException("Review.NotFound", $"Review '{command.ReviewId}' was not found.");

            if (review.ReviewerId != command.RequestingUserId)
                throw new DomainException("Review.NotOwned", "You can only update your own review.");

            review.UpdateRating(command.Rating);
            review.UpdateComment(command.Comment);
            await _reviewRepository.UpdateAsync(review, ct);

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
