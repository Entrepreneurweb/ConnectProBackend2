using MediatR;
using Social.Application.Dtos;
using Social.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Application.Queries.GetUserLikedServices
{
    public sealed class GetUserLikedServicesHandler(ILikeRepository likeRepository)
    : IRequestHandler<GetUserLikedServicesQuery, IReadOnlyList<LikedServiceDto>>
    {
        public async Task<IReadOnlyList<LikedServiceDto>> Handle(
            GetUserLikedServicesQuery query, CancellationToken ct)
        {
            return await likeRepository.GetLikedServicesByUserAsync(query.UserId, ct);
        }
    }
}
