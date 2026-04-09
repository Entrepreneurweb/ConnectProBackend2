using Marketplace.Domain.Aggregates.Service.Entities;
using Marketplace.Domain.Aggregates.Service.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Infrastructure.Persistence.Repositories
{
    public sealed class ServiceProjectionRepository : IServiceProjectionRepository
    {
        private readonly MarketPlaceDbContext _context;

        public ServiceProjectionRepository(MarketPlaceDbContext context)
            => _context = context;

        public async Task<ServiceProjection?> GetByServiceIdAsync(Guid serviceId, CancellationToken cancellationToken = default)
            => await _context.ServiceProjections
                .FirstOrDefaultAsync(s => s.ServiceId  /*.Value*/ == serviceId, cancellationToken);

        public async Task AddAsync(ServiceProjection projection, CancellationToken cancellationToken = default)
        {
            await _context.ServiceProjections.AddAsync(projection, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(ServiceProjection projection, CancellationToken cancellationToken = default)
        {
            _context.ServiceProjections.Update(projection);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
