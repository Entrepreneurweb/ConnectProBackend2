using MediatR;
using Portfolio.Application.Reviews.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Reviews.Commands.UpdateReview
{
    public record UpdateReviewCommand(
       Guid ReviewId,
       Guid RequestingUserId,
       int Rating,
       string Comment
   ) : IRequest<ReviewDto>;

}
