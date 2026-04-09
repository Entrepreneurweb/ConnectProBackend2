using Marketplace.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Application.JobPosts.Queries.GetJobPostById
{
    public sealed record GetJobPostByIdQuery(Guid JobPostId) : IRequest<JobPostDto?>;
}
