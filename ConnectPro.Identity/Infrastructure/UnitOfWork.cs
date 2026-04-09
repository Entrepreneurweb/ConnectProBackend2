using ConnectPro.Identity.Domain;
using ConnectPro.Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
        private readonly IdentityAppDbContext _context;

        public IUserRepository Users { get; }

        public UnitOfWork(IdentityAppDbContext context, IUserRepository userRepository)
        {
            _context = context;
            Users = userRepository;
        }

        public Task<int> CommitAsync(CancellationToken ct = default)
            => _context.SaveChangesAsync(ct);
    }
}
