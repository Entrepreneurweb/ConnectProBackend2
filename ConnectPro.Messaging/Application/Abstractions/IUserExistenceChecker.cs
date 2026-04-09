using ConnectPro.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Application.Abstractions
{
    public interface IUserExistenceChecker
    {
        Task<bool> ExistsAsync(Guid userId, CancellationToken ct = default);
    }
}
