using ConnectPro.SharedKernel;
using ConnectPro.SharedKernel.Exceptions;
using Social.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Domain.Aggregates.Like.Entities
{
    public sealed class Like : AggregateRoot<Like>
    {
        public Guid Id { get; }
        public Guid UserId { get; }
        public Guid ServiceId { get; }
        public Guid ServiceOwnerId { get; }
        public DateTime CreatedAt { get; }

        private Like() { }

        private Like(Guid userId, Guid serviceId, Guid serviceOwnerId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            ServiceId = serviceId;
            ServiceOwnerId = serviceOwnerId;
            CreatedAt = DateTime.UtcNow;
        }

        public static Like Create(Guid userId, Guid serviceId, Guid serviceOwnerId)
        {
            if (userId == serviceOwnerId)
                throw new DomainException("Cannot like own service");

            var like = new Like(userId, serviceId, serviceOwnerId);
           // like.AddDomainEvent(new ServiceLiked(userId, serviceId));
            return like;
        }

        public void Remove()
        {
          //  AddDomainEvent(new ServiceUnliked(UserId, ServiceId));
        }
    }
}
