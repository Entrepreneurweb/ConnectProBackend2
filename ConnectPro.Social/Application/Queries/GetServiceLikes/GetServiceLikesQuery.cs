using MediatR;
using Social.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Application.Queries.GetServiceLikes
{
    public sealed record GetServiceLikesQuery(Guid ServiceId)
        : IRequest<IReadOnlyList<ServiceLikeDto>>;
}
