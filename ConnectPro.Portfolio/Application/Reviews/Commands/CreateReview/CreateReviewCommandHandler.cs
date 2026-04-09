using ConnectPro.SharedKernel.Exceptions;
using MediatR;
using Portfolio.Application.Reviews.DTO;
using Portfolio.Domain.Aggregates.Portfolio.Repository;
using Portfolio.Domain.Aggregates.Review;
using Portfolio.Domain.Aggregates.Service.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Portfolio.Application.Reviews.Commands.CreateReview
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ReviewDto>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IReviewRepository _reviewRepository;

        public CreateReviewCommandHandler(
            IServiceRepository serviceRepository,
            IPortfolioRepository portfolioRepository,
            IReviewRepository reviewRepository)
        {
            _serviceRepository = serviceRepository;
            _portfolioRepository = portfolioRepository;
            _reviewRepository = reviewRepository;
        }

        public async Task<ReviewDto> Handle(CreateReviewCommand command, CancellationToken ct)
        {
            var service = await _serviceRepository.GetByIdAsync(command.ServiceId, ct);
            if (service is null)
                throw new DomainException("service not found");

            // Le propriétaire du service ne peut pas se reviewer lui-même
            var portfolio = await _portfolioRepository.GetByIdAsync(service.PortfolioId, ct);
            if (portfolio!.GetOwnerId == command.ReviewerId)
                throw new DomainException("Review.SelfReview", "You cannot review your own service.");

            // Un reviewer ne peut laisser qu'une seule review par service
            var existing = await _reviewRepository.GetByServiceAndReviewerAsync(command.ServiceId, command.ReviewerId, ct);
            if (existing is not null)
                throw new DomainException("Review.AlreadyExists", "You have already reviewed this service.");

            var review = Review.Create(command.ServiceId, command.ReviewerId, command.Rating, command.Comment);
            await _reviewRepository.AddAsync(review, ct);

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
