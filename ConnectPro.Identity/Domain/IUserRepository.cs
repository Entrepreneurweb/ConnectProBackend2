using ConnectPro.Identity.Domain.Aggregates.ValueObjects;
using ConnectPro.SharedKernel;
using Identity.Domain.Aggregates.User.Entities;
using Identity.Domain.Aggregates.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectPro.Identity.Domain
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Id<User> id, CancellationToken ct = default);
        Task<User?> GetByEmailAsync(Email email, CancellationToken ct = default);
        Task<bool> ExistsByEmailAsync(Email email, CancellationToken ct = default);
        Task AddAsync(User user, CancellationToken ct = default);
        Task UpdateAsync(User user, CancellationToken ct = default);
        Task  SendOtpSenderService(Email email, OtpCode otpCode, CancellationToken ct = default);
    }
}
