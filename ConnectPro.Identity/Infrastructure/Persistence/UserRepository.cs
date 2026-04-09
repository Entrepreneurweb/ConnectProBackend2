using ConnectPro.Identity.Domain;
using ConnectPro.Identity.Domain.Aggregates.ValueObjects;
using ConnectPro.SharedKernel;
using Identity.Domain.Aggregates.User.Entities;
using Identity.Domain.Aggregates.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectPro.Identity.Infrastructure.Persistence
{
    internal sealed class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
            => _userManager = userManager;

        public async Task<User?> GetByIdAsync(Id<User> id, CancellationToken ct = default)
        {
            var appUser = await _userManager.FindByIdAsync(id.Value.ToString());
            return appUser?.ToDomain();
        }

        public async Task<User?> GetByEmailAsync(Email email, CancellationToken ct = default)
        {
            var appUser = await _userManager.FindByEmailAsync(email.Value);
            return appUser?.ToDomain();
        }

        public async Task<bool> ExistsByEmailAsync(Email email, CancellationToken ct = default)
        {
            return await _userManager.Users
                .AsNoTracking()
                .AnyAsync(u => u.NormalizedEmail == email.Value.ToUpperInvariant(), ct);
        }

        public async Task AddAsync(User user, CancellationToken ct = default)
        {
            var appUser = ApplicationUser.FromDomain(user);

            // Le hash est déjà calculé dans la couche Application
            // On bypasse le hashing de UserManager en assignant directement
            var result = await _userManager.CreateAsync(appUser);

            if (!result.Succeeded)
                throw new InvalidOperationException(
                    $"Erreur lors de la création de l'utilisateur : {string.Join(", ", result.Errors.Select(e => e.Description))}"
                );
        }

        public async Task UpdateAsync(User user, CancellationToken ct = default)
        {
            var appUser = await _userManager.FindByIdAsync(user.Id.Value.ToString())
                          ?? throw new InvalidOperationException($"ApplicationUser '{user.Id}' introuvable.");

            appUser.SyncFromDomain(user);

            var result = await _userManager.UpdateAsync(appUser);

            if (!result.Succeeded)
                throw new InvalidOperationException(
                    $"Erreur lors de la mise à jour de l'utilisateur : {string.Join(", ", result.Errors.Select(e => e.Description))}"
                );
        }

        public Task SendOtpSenderService(Email email, OtpCode otpCode, CancellationToken ct = default)
        {
             Console.WriteLine($"Envoi de l'OTP '{otpCode}' à l'email '{email}'");
            return Task.Delay(500, ct); 
        }
    }
}
