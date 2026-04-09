using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Reviews.Commands.DeleteReview
{
    public record DeleteReviewCommand(
     Guid ReviewId,
     Guid RequestingUserId
 ) : IRequest;

}
