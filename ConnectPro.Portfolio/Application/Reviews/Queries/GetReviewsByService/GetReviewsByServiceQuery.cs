using MediatR;
using Portfolio.Application.Reviews.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Reviews.Queries.GetReviewsByService
{
    public record GetReviewsByServiceQuery(Guid ServiceId) : IRequest<IReadOnlyList<ReviewDto>>;
}
