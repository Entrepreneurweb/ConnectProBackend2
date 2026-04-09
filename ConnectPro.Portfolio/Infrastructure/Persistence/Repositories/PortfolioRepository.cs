using ConnectPro.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Aggregates.Portfolio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioEntity = Portfolio.Domain.Aggregates.Portfolio.Entities.Portfolio;

namespace Portfolio.Infrastructure.Persistence.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly PortfolioDbContext _context;

        public PortfolioRepository(PortfolioDbContext context)
        {
            _context = context;
        }

        public async Task<PortfolioEntity?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var portfolioId = new Id<PortfolioEntity>(id);
            var response =  await _context.Portfolios
                .FirstOrDefaultAsync(p => p.Id == portfolioId, ct);

            return response;
        }

        public async Task<PortfolioEntity?> GetByIdWithFullDetailsAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Portfolios
                .Include("_generalInfo")
                .Include("_contactInfo")
                .Include("_locationInfo")
                .Include("_professionalInfo")
                .Include("_professionalInfo._skills")
                .Include("_socialLinks")
                .Include("_experiences")
                .Include("_certifications")
                .FirstOrDefaultAsync(p => p.Id.Value == id, ct);
        }

        public async Task<PortfolioEntity?> GetByOwnerIdAsync(Guid ownerId, CancellationToken ct = default)
        {
            return await _context.Portfolios
                .FirstOrDefaultAsync(p => EF.Property<Guid>(p, "_ownerId") == ownerId, ct);
        }

        public async Task AddAsync(PortfolioEntity portfolio, CancellationToken ct = default)
        {
            await _context.Portfolios.AddAsync(portfolio, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(PortfolioEntity portfolio, CancellationToken ct = default)
        {
            _context.Portfolios.Update(portfolio);
            await _context.SaveChangesAsync(ct);
        }
    }


}
