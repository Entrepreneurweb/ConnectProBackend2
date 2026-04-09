using ConnectPro.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Aggregates.Service.Entities;
using Portfolio.Domain.Aggregates.Service.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Infrastructure.Persistence.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly PortfolioDbContext _context;

        public ServiceRepository(PortfolioDbContext context)
        {
            _context = context;
        }

        public async Task<Service?> GetByIdAsync(Id<Service> id, CancellationToken ct = default)
        {
            
            return await _context.Services
                .FirstOrDefaultAsync(s => s.Id == id, ct);
        }

        public async Task<Service?> GetByIdWithFullDetailsAsync(Id<Service> id, CancellationToken ct = default)
        {
            return await _context.Services
                .Include("_tags")
                .Include("_imageUrls")
                .Include("_faqs")
                .Include("_awards")
                .FirstOrDefaultAsync(s => s.Id == id, ct);
        }

        public async Task<IReadOnlyList<Service>> GetByPortfolioIdAsync(Guid portfolioId, CancellationToken ct = default)
        {
            return await _context.Services
                .Where(s => EF.Property<Guid>(s, "_portfolioId") == portfolioId)
                .ToListAsync(ct);
        }

        public async Task AddAsync(Service service, CancellationToken ct = default)
        {
            await _context.Services.AddAsync(service, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Service service, CancellationToken ct = default)
        {
            _context.Services.Update(service);
            await Task.CompletedTask;
               //await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Service service, CancellationToken ct = default)
        {
            _context.Services.Remove(service);
            await _context.SaveChangesAsync(ct);
        }

         
    }

}


