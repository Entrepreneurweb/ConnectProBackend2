using ConnectPro.SharedKernel.Exceptions;
using MediatR;
using Social.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Application.Commands.UnfollowPortfolio
{
    public sealed class UnfollowPortfolioHandler(IFollowRepository followRepository)
   : IRequestHandler<UnfollowPortfolioCommand, Unit>
    {
        public async Task<Unit> Handle(UnfollowPortfolioCommand command, CancellationToken ct)
        {
            var follow = await followRepository.GetAsync(command.FollowerId, command.PortfolioId, ct)
                ?? throw new DomainException("Not following this portfolio");

            follow.Remove();

            await followRepository.RemoveAsync(follow, ct);

            return Unit.Value;
        }
    }
}
