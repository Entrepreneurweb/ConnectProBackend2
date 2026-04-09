using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Aggregates.Portfolio.ValueObject
{
    public record PhoneNumber
    {
        public string Value { get; }

        private PhoneNumber() { }

        private PhoneNumber(string value)
        {
            Value = value;
        }

        public static PhoneNumber Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Phone number is required.");

            var digits = value.Replace(" ", "").Replace("-", "").Replace("+", "");
            if (!digits.All(char.IsDigit))
                throw new ArgumentException("Phone number contains invalid characters.");
            if (digits.Length < 7 || digits.Length > 15)
                throw new ArgumentException("Phone number length is invalid.");

            return new PhoneNumber(value.Trim());
        }

        public override string ToString() => Value;
    }

}
