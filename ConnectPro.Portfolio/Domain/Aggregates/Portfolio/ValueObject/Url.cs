using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Aggregates.Portfolio.ValueObject
{
    public record Url
    {
        public string Value { get; }

        private Url() { }

        private Url(string value)
        {
            Value = value;
        }

        public static Url Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Url is required.");
            if (!Uri.TryCreate(value, UriKind.Absolute, out var uri) ||
                (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
                throw new ArgumentException("Url is invalid.");

            return new Url(value.Trim());
        }

        public override string ToString() => Value;
    }
}
