using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Application.Dtos
{
    public sealed record FeedDto(
     Guid ClientId,
     IReadOnlyCollection<FeedItemDto> Items
 );

    public sealed record FeedItemDto(
        Guid RefId,
        string RefType,
        double Score
    );

}
