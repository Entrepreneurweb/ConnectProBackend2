using ConnectPro.Identity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        Task<int> CommitAsync(CancellationToken ct = default);
    }
}
