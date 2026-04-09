using ConnectPro.SharedKernel.Exceptions;
using MediatR;
using Portfolio.Domain.Aggregates.Portfolio.Repository;
using Portfolio.Domain.Aggregates.Review;
using Portfolio.Domain.Aggregates.Service.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Portfolio.Application.Reviews.Commands.HideReview
{
    public class HideReviewCommandHandler : IRequestHandler<HideReviewCommand>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IPortfolioRepository _portfolioRepository;

        public HideReviewCommandHandler(
            IReviewRepository reviewRepository,
            IServiceRepository serviceRepository,
            IPortfolioRepository portfolioRepository)
        {
            _reviewRepository = reviewRepository;
            _serviceRepository = serviceRepository;
            _portfolioRepository = portfolioRepository;
        }

        public async Task Handle(HideReviewCommand command, CancellationToken ct)
        {
            var review = await _reviewRepository.GetByIdAsync(command.ReviewId, ct);
            if (review is null)
                throw new DomainException("Review.NotFound", $"Review '{command.ReviewId}' was not found.");

            // Seul le propriétaire du service peut masquer une review
            var service = await _serviceRepository.GetByIdAsync(review.ServiceId, ct);
            var portfolio = await _portfolioRepository.GetByIdAsync(service!.PortfolioId, ct);
            if (portfolio!.GetOwnerId != command.RequestingUserId)
                throw new DomainException("Review.NotAuthorized", "Only the service owner can hide a review.");

            review.Hide();
            await _reviewRepository.UpdateAsync(review, ct);
        }
    }
}
