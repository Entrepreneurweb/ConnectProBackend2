using ConnectPro.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Aggregates.Service.Entities
{
    public class Tag : Entity<Tag>
    {
 
        public Id<Service> ServiceId { get; private set; }
        private string _value;

        private Tag() { }

        private Tag(Guid serviceId, string value)
        {
            Id = Guid.NewGuid();
            ServiceId = serviceId;
            _value = value;
        }

        public static Tag Create(Guid serviceId, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Tag value is required.");
            if (value.Length > 50)
                throw new ArgumentException("Tag must not exceed 50 characters.");
            if (value.Contains(' '))
                throw new ArgumentException("Tag must not contain spaces.");

            return new Tag(serviceId, value.Trim().ToLower());
        }

        public string Value => _value;
    }
}
