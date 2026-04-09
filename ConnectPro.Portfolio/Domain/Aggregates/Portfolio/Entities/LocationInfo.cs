using ConnectPro.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Aggregates.Portfolio.Entities
{
    public class LocationInfo : Entity<LocationInfo>
    {
      //  public Guid Id { get; private set; }
        public Id<Portfolio> PortfolioId { get; private set; }
        private string _country;
        private string _city;
        private string? _timezone;

        private LocationInfo() { }

        private LocationInfo(Guid portfolioId, string country, string city, string? timezone)
        {
            Id = Guid.NewGuid();
            PortfolioId = portfolioId;
            _country = country;
            _city = city;
            _timezone = timezone;
        }

        public static LocationInfo Create(Guid portfolioId, string country, string city, string? timezone)
        {
            if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException("Country is required.");
            if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("City is required.");

            return new LocationInfo(portfolioId, country.Trim(), city.Trim(), timezone?.Trim());
        }

        public void UpdateCountry(string country)
        {
            if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException("Country is required.");
            _country = country.Trim();
        }

        public void UpdateCity(string city)
        {
            if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("City is required.");
            _city = city.Trim();
        }

        public void UpdateTimezone(string? timezone) => _timezone = timezone?.Trim();

        public string Country => _country;
        public string City => _city;
        public string? Timezone => _timezone;
    }
}
