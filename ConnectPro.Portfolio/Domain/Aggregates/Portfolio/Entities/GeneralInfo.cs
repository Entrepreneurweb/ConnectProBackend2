using ConnectPro.SharedKernel;
using Portfolio.Domain.Aggregates.Portfolio.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Aggregates.Portfolio.Entities
{
    public class GeneralInfo : Entity<GeneralInfo>
    {
        public Id<Portfolio> PortfolioId { get; private set; }
        private string _firstName;
        private string _lastName;
        private string? _bio;
        private Url? _avatarUrl;

        private GeneralInfo() { }

        private GeneralInfo(Guid portfolioId, string firstName, string lastName, string? bio, Url? avatarUrl)
        {
            Id = Guid.NewGuid();
            PortfolioId = portfolioId;
            _firstName = firstName;
            _lastName = lastName;
            _bio = bio;
            _avatarUrl = avatarUrl;
        }

        public static GeneralInfo Create(Guid portfolioId, string firstName, string lastName, string? bio, string? avatarUrl)
        {
            if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("FirstName is required.");
            if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("LastName is required.");
            if (bio?.Length > 500) throw new ArgumentException("Bio must not exceed 500 characters.");

            return new GeneralInfo(
                portfolioId,
                firstName.Trim(),
                lastName.Trim(),
                bio?.Trim(),
                avatarUrl is not null ? Url.Create(avatarUrl) : null
            );
        }

        public void UpdateFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("FirstName is required.");
            _firstName = firstName.Trim();
        }

        public void UpdateLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("LastName is required.");
            _lastName = lastName.Trim();
        }

        public void UpdateBio(string? bio)
        {
            if (bio?.Length > 500) throw new ArgumentException("Bio must not exceed 500 characters.");
            _bio = bio?.Trim();
        }

        public void UpdateAvatarUrl(string? avatarUrl) =>
            _avatarUrl = avatarUrl is not null ? Url.Create(avatarUrl) : null;

        public string FirstName => _firstName;
        public string LastName => _lastName;
        public string? Bio => _bio;
        public Url? AvatarUrl => _avatarUrl;
    }
}
