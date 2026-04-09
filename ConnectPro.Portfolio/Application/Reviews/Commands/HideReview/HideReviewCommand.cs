using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Reviews.Commands.HideReview
{
    public record HideReviewCommand(
     Guid ReviewId,
     Guid RequestingUserId  // doit être le propriétaire du service
 ) : IRequest;

}
