using ConnectPro.SharedKernel;
using Portfolio.Domain.Aggregates.Portfolio.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Aggregates.Portfolio.Entities
{
    public class ContactInfo: Entity<ContactInfo>
    {
        public Id<Portfolio> PortfolioId { get; private set; }
        private Email _email;
        private PhoneNumber? _phone;
        private Url? _website;

        private ContactInfo() { }

        private ContactInfo(Guid portfolioId, Email email, PhoneNumber? phone, Url? website)
        {
            Id = Guid.NewGuid();
            PortfolioId = portfolioId;
            _email = email;
            _phone = phone;
            _website = website;
        }

        public static ContactInfo Create(Guid portfolioId, string email, string? phone, string? website)
        {
            return new ContactInfo(
                portfolioId,
                Email.Create(email),
                phone is not null ? PhoneNumber.Create(phone) : null,
                website is not null ? Url.Create(website) : null
            );
        }

        public void UpdateEmail(string email) => _email = Email.Create(email);
        public void UpdatePhone(string? phone) => _phone = phone is not null ? PhoneNumber.Create(phone) : null;
        public void UpdateWebsite(string? website) => _website = website is not null ? Url.Create(website) : null;

        public Email Email => _email;
        public PhoneNumber? Phone => _phone;
        public Url? Website => _website;
    }
}
