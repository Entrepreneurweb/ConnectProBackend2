using ConnectPro.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioEntity = Portfolio.Domain.Aggregates.Portfolio.Entities.Portfolio ;


namespace Portfolio.Domain.Aggregates.Portfolio.Repository
{

    public interface IPortfolioRepository
    {
        Task<PortfolioEntity?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<PortfolioEntity?> GetByIdWithFullDetailsAsync(Guid id, CancellationToken ct = default); // charge toutes les entités internes
        Task<PortfolioEntity?> GetByOwnerIdAsync(Guid ownerId, CancellationToken ct = default);
        Task AddAsync(PortfolioEntity portfolio, CancellationToken ct = default);
        Task UpdateAsync(PortfolioEntity portfolio, CancellationToken ct = default);
    }
}
