using MediatR;
using Portfolio.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Services.Queries.GetServiceById
{
    public record GetServiceByIdQuery(Guid ServiceId) : IRequest<ServiceFullDto>;
}
