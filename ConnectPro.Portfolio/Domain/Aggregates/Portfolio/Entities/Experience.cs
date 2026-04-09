using ConnectPro.SharedKernel;
using Portfolio.Domain.Aggregates.Portfolio.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DateRange = Portfolio.Domain.Aggregates.Portfolio.ValueObject.DateRange;

namespace Portfolio.Domain.Aggregates.Portfolio.Entities
{
    public class Experience : Entity<Experience>
    {
        public Id<Portfolio> PortfolioId { get; private set; }
        private string _company;
        private string _role;
        private string? _description;
        private DateRange _period;

        private Experience() { }

        private Experience(Guid portfolioId, string company, string role, string? description, DateRange period)
        {
            Id = Guid.NewGuid();
            PortfolioId = portfolioId;
            _company = company;
            _role = role;
            _description = description;
            _period = period;
        }

        public static Experience Create(Guid portfolioId, string company, string role, string? description, DateOnly start, DateOnly? end)
        {
            if (string.IsNullOrWhiteSpace(company)) throw new ArgumentException("Company is required.");
            if (string.IsNullOrWhiteSpace(role)) throw new ArgumentException("Role is required.");

            return new Experience(portfolioId, company.Trim(), role.Trim(), description?.Trim(), DateRange.Create(start, end));
        }

        public void UpdateCompany(string company)
        {
            if (string.IsNullOrWhiteSpace(company)) throw new ArgumentException("Company is required.");
            _company = company.Trim();
        }

        public void UpdateRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role)) throw new ArgumentException("Role is required.");
            _role = role.Trim();
        }

        public void UpdateDescription(string? description) => _description = description?.Trim();

        public void UpdatePeriod(DateOnly start, DateOnly? end) => _period = DateRange.Create(start, end);

        public string Company => _company;
        public string Role => _role;
        public string? Description => _description;
        public DateRange Period => _period;
    }
}
