using ConnectPro.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Aggregates.Service.Entities
{
    public class Award : Entity<Award>
    {
        public Id<Service> ServiceId { get; private set; }
        private string _title;
        private string _issuedBy;
        private DateOnly _issuedAt;
        private string? _description;

        private Award() { }

        private Award(Guid serviceId, string title, string issuedBy, DateOnly issuedAt, string? description)
        {
            Id = Guid.NewGuid();
            ServiceId = serviceId;
            _title = title;
            _issuedBy = issuedBy;
            _issuedAt = issuedAt;
            _description = description;
        }

        public static Award Create(Guid serviceId, string title, string issuedBy, DateOnly issuedAt, string? description)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title is required.");
            if (string.IsNullOrWhiteSpace(issuedBy)) throw new ArgumentException("IssuedBy is required.");

            return new Award(serviceId, title.Trim(), issuedBy.Trim(), issuedAt, description?.Trim());
        }

        public void UpdateTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title is required.");
            _title = title.Trim();
        }

        public void UpdateIssuedBy(string issuedBy)
        {
            if (string.IsNullOrWhiteSpace(issuedBy)) throw new ArgumentException("IssuedBy is required.");
            _issuedBy = issuedBy.Trim();
        }

        public void UpdateIssuedAt(DateOnly issuedAt) => _issuedAt = issuedAt;
        public void UpdateDescription(string? description) => _description = description?.Trim();

        public string Title => _title;
        public string IssuedBy => _issuedBy;
        public DateOnly IssuedAt => _issuedAt;
        public string? Description => _description;
    }
}
