using ConnectPro.SharedKernel.Exceptions;
using MediatR;
using Portfolio.Domain.Aggregates.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Portfolio.Application.Reviews.Commands.DeleteReview
{
    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand>
    {
        private readonly IReviewRepository _reviewRepository;

        public DeleteReviewCommandHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task Handle(DeleteReviewCommand command, CancellationToken ct)
        {
            var review = await _reviewRepository.GetByIdAsync(command.ReviewId, ct);
            if (review is null)
                throw new DomainException("Review.NotFound", $"Review '{command.ReviewId}' was not found.");

            if (review.ReviewerId != command.RequestingUserId)
                throw new DomainException("Review.NotOwned", "You can only delete your own review.");

            await _reviewRepository.DeleteAsync(review, ct);
        }
    }
}
