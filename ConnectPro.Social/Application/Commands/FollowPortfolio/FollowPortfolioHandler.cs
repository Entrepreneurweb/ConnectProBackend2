using ConnectPro.SharedKernel.Exceptions;
using MediatR;
using Social.Domain.Aggregates.Follow.Entities;
using Social.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Application.Commands.FollowPortfolio
{
    public sealed class FollowPortfolioHandler(IFollowRepository followRepository)
   : IRequestHandler<FollowPortfolioCommand, Unit>
    {
        public async Task<Unit> Handle(FollowPortfolioCommand command, CancellationToken ct)
        {
            var existing = await followRepository.GetAsync(command.FollowerId, command.PortfolioId, ct);
            if (existing is not null)
                throw new DomainException("Already following this portfolio");

            var follow = Follow.Create(command.FollowerId, command.PortfolioId, command.PortfolioOwnerId);

            await followRepository.AddAsync(follow, ct);

            return Unit.Value;
        }
    }
}
