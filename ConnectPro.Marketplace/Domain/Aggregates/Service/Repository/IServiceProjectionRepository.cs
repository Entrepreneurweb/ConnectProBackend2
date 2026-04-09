using Marketplace.Domain.Aggregates.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain.Aggregates.Service.Repository
{
    public interface IServiceProjectionRepository
    {
        Task<ServiceProjection?> GetByServiceIdAsync(Guid serviceId, CancellationToken cancellationToken = default);
        Task AddAsync(ServiceProjection projection, CancellationToken cancellationToken = default);
        Task UpdateAsync(ServiceProjection projection, CancellationToken cancellationToken = default);
        //Task<ServiceProjection> Create(Guid serviceId, Guid portfolioId, Guid requestingUserId, string title, string description);
       
    }
}
