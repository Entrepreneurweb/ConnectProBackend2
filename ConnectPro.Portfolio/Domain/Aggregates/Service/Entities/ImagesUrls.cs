using ConnectPro.SharedKernel;
using Portfolio.Domain.Aggregates.Portfolio.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Aggregates.Service.Entities
{
    public class ImageUrl : Entity<ImageUrl>
    {
        public Id<Service> ServiceId { get; private set; }
        private Url _value;

        private ImageUrl() { }

        private ImageUrl(Guid serviceId, Url value)
        {
            Id = Guid.NewGuid();
            ServiceId = serviceId;
            _value = value;
        }

        public static ImageUrl Create(Guid serviceId, string url)
        {
            return new ImageUrl(serviceId, Url.Create(url));
        }

        public Url Value => _value;
    }
}
