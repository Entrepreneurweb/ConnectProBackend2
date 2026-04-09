using ConnectPro.SharedKernel;
using Portfolio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Aggregates.Review
{
    public class Review : AggregateRoot<Review>
    {
        private Guid _serviceId;
        private Guid _reviewerId;
        private Rating _rating;
        private string _comment;
        private ReviewStatus _status;

        private Review() { }

        private Review(Guid serviceId, Guid reviewerId, Rating rating, string comment)
        {
            _serviceId = serviceId;
            _reviewerId = reviewerId;
            _rating = rating;
            _comment = comment;
            _status = ReviewStatus.Published;
        }

        public static Review Create(Guid serviceId, Guid reviewerId, int rating, string comment)
        {
            if (string.IsNullOrWhiteSpace(comment))
                throw new ArgumentException("Comment is required.");
            if (comment.Length > 1000)
                throw new ArgumentException("Comment must not exceed 1000 characters.");

            return new Review(serviceId, reviewerId, Rating.Create(rating), comment.Trim());
        }

        public void UpdateComment(string comment)
        {
            if (string.IsNullOrWhiteSpace(comment))
                throw new ArgumentException("Comment is required.");
            if (comment.Length > 1000)
                throw new ArgumentException("Comment must not exceed 1000 characters.");
            _comment = comment.Trim();
        }

        public void UpdateRating(int rating) => _rating = Rating.Create(rating);

        public void Hide()
        {
            if (_status == ReviewStatus.Hidden)
                throw new InvalidOperationException("Review is already hidden.");
            _status = ReviewStatus.Hidden;
        }

        public void Publish()
        {
            if (_status == ReviewStatus.Published)
                throw new InvalidOperationException("Review is already published.");
            _status = ReviewStatus.Published;
        }

        public Guid ServiceId => _serviceId;
        public Guid ReviewerId => _reviewerId;
        public Rating Rating => _rating;
        public string Comment => _comment;
        public ReviewStatus Status => _status;
    }
}
