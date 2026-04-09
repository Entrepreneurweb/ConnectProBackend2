using ConnectPro.SharedKernel;
using Portfolio.Domain.Aggregates.Portfolio.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Aggregates.Portfolio.Entities
{
    public class Certification : Entity<Certification>
    {
    
        public Id<Portfolio> PortfolioId { get; private set; }
        private string _name;
        private string _issuingOrganization;
        private DateOnly _issueDate;
        private DateOnly? _expiryDate;
        private Url? _credentialUrl;

        private Certification() { }

        private Certification(Guid portfolioId, string name, string issuingOrganization, DateOnly issueDate, DateOnly? expiryDate, Url? credentialUrl)
        {
            Id = Guid.NewGuid();
            PortfolioId = portfolioId;
            _name = name;
            _issuingOrganization = issuingOrganization;
            _issueDate = issueDate;
            _expiryDate = expiryDate;
            _credentialUrl = credentialUrl;
        }

        public static Certification Create(Guid portfolioId, string name, string issuingOrganization, DateOnly issueDate, DateOnly? expiryDate, string? credentialUrl)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Certification name is required.");
            if (string.IsNullOrWhiteSpace(issuingOrganization)) throw new ArgumentException("Issuing organization is required.");
            if (expiryDate.HasValue && expiryDate.Value < issueDate) throw new ArgumentException("Expiry date must be after issue date.");

            return new Certification(
                portfolioId,
                name.Trim(),
                issuingOrganization.Trim(),
                issueDate,
                expiryDate,
                credentialUrl is not null ? Url.Create(credentialUrl) : null
            );
        }

        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Certification name is required.");
            _name = name.Trim();
        }

        public void UpdateIssuingOrganization(string issuingOrganization)
        {
            if (string.IsNullOrWhiteSpace(issuingOrganization)) throw new ArgumentException("Issuing organization is required.");
            _issuingOrganization = issuingOrganization.Trim();
        }

        public void UpdateIssueDate(DateOnly issueDate)
        {
            if (_expiryDate.HasValue && _expiryDate.Value < issueDate)
                throw new ArgumentException("Expiry date must be after issue date.");
            _issueDate = issueDate;
        }

        public void UpdateExpiryDate(DateOnly? expiryDate)
        {
            if (expiryDate.HasValue && expiryDate.Value < _issueDate)
                throw new ArgumentException("Expiry date must be after issue date.");
            _expiryDate = expiryDate;
        }

        public void UpdateCredentialUrl(string? credentialUrl) =>
            _credentialUrl = credentialUrl is not null ? Url.Create(credentialUrl) : null;

        public string Name => _name;
        public string IssuingOrganization => _issuingOrganization;
        public DateOnly IssueDate => _issueDate;
        public DateOnly? ExpiryDate => _expiryDate;
        public Url? CredentialUrl => _credentialUrl;
    }
}
