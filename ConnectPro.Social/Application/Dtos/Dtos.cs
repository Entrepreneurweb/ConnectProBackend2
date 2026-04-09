using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Application.Dtos
{
    public sealed record ServiceLikeDto(Guid UserId, DateTime LikedAt);
    public sealed record LikedServiceDto(Guid ServiceId, DateTime LikedAt);
}
