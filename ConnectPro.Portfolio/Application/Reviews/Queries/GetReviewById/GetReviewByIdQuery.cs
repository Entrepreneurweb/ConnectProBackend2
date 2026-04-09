using MediatR;
using Portfolio.Application.Reviews.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Reviews.Queries.GetReviewById
{
    public record GetReviewByIdQuery(Guid ReviewId) : IRequest<ReviewDto>;
}
