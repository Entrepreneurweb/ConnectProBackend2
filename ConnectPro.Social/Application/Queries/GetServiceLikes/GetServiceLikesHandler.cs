using MediatR;
using Social.Application.Dtos;
using Social.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Application.Queries.GetServiceLikes
{
    public sealed class GetServiceLikesHandler(ILikeRepository likeRepository)
    : IRequestHandler<GetServiceLikesQuery, IReadOnlyList<ServiceLikeDto>>
    {
        public async Task<IReadOnlyList<ServiceLikeDto>> Handle(
            GetServiceLikesQuery query, CancellationToken ct)
        {
            return await likeRepository.GetLikesByServiceAsync(query.ServiceId, ct);
        }
    }
}
