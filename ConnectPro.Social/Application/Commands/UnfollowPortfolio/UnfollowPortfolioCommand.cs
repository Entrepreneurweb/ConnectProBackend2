using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Application.Commands.UnfollowPortfolio
{
    public sealed record UnfollowPortfolioCommand(
    Guid FollowerId,
    Guid PortfolioId) : IRequest<Unit>;
}
