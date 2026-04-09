using ConnectPro.SharedKernel.Exceptions;
using MediatR;
using Social.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Application.Commands.UnlikeService
{
    public sealed class UnlikeServiceHandler(ILikeRepository likeRepository)
    : IRequestHandler<UnlikeServiceCommand, Unit>
    {
        public async Task<Unit> Handle(UnlikeServiceCommand command, CancellationToken ct)
        {
            var like = await likeRepository.GetAsync(command.UserId, command.ServiceId, ct)
                ?? throw new DomainException("Not liked this service");

            like.Remove();

            await likeRepository.RemoveAsync(like, ct);

            return Unit.Value;
        }
    }
}
