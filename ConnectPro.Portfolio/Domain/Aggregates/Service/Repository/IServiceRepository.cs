using ConnectPro.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceEntity = Portfolio.Domain.Aggregates.Service.Entities.Service;
using PortfolioEntity = Portfolio.Domain.Aggregates.Portfolio.Entities.Portfolio;

namespace Portfolio.Domain.Aggregates.Service.Repository
{
    public interface IServiceRepository
    {
        Task<ServiceEntity?> GetByIdAsync(Id<ServiceEntity> id, CancellationToken ct = default);
        Task<IReadOnlyList<ServiceEntity>> GetByPortfolioIdAsync(Guid portfolioId, CancellationToken ct = default);
        Task AddAsync(ServiceEntity service, CancellationToken ct = default);
        Task UpdateAsync(ServiceEntity service, CancellationToken ct = default);
        Task<ServiceEntity> GetByIdWithFullDetailsAsync(Id<ServiceEntity>  serviceId, CancellationToken ct);
        Task DeleteAsync(ServiceEntity service, CancellationToken ct);
    }
}
