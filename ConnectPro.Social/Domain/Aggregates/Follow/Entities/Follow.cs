using ConnectPro.SharedKernel;
using ConnectPro.SharedKernel.Exceptions;
using Social.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Domain.Aggregates.Follow.Entities
{
    public sealed class Follow : AggregateRoot<Follow>
    {
        public Guid Id { get; }
        public Guid FollowerId { get; }
        public Guid PortfolioId { get; }
        public Guid PortfolioOwnerId { get; }
        public DateTime CreatedAt { get; }

        private Follow() { }

        private Follow(Guid followerId, Guid portfolioId, Guid portfolioOwnerId)
        {
            Id = Guid.NewGuid();
            FollowerId = followerId;
            PortfolioId = portfolioId;
            PortfolioOwnerId = portfolioOwnerId;
            CreatedAt = DateTime.UtcNow;
        }

        public static Follow Create(Guid followerId, Guid portfolioId, Guid portfolioOwnerId)
        {
            if (followerId == portfolioOwnerId)
                throw new DomainException("Cannot follow own portfolio");

            var follow = new Follow(followerId, portfolioId, portfolioOwnerId);
           // follow.AddDomainEvent(new PortfolioFollowed(followerId, portfolioId));
            return follow;
        }

        public void Remove()
        {
       //     AddDomainEvent(new PortfolioUnfollowed(FollowerId, PortfolioId));
        }
    }
}
