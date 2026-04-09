using ConnectPro.SharedKernel.Exceptions;
using MediatR;
using Social.Domain.Aggregates.Like.Entities;
using Social.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Application.Commands.LikeService
{
    public sealed class LikeServiceHandler(ILikeRepository likeRepository)
     : IRequestHandler<LikeServiceCommand, Unit>
    {
        public async Task<Unit> Handle(LikeServiceCommand command, CancellationToken ct)
        {
            var existing = await likeRepository.GetAsync(command.UserId, command.ServiceId, ct);
            if (existing is not null)
                throw new DomainException("Already liked this service");

            var like = Like.Create(command.UserId, command.ServiceId, command.ServiceOwnerId);

            await likeRepository.AddAsync(like, ct);

            return Unit.Value;
        }
    }
}
