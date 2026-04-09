using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Application.Commands.FollowPortfolio
{
    public sealed record FollowPortfolioCommand(
    Guid FollowerId,
    Guid PortfolioId,
    Guid PortfolioOwnerId) : IRequest<Unit>;
}
