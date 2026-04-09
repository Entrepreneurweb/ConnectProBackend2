using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Aggregates.Portfolio.ValueObject
{
    public record Email
    {
        public string Value { get; }

        private Email() { }

        private Email(string value)
        {
            Value = value;
        }

        public static Email Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Email is required.");
            if (!value.Contains('@') || !value.Contains('.'))
                throw new ArgumentException("Email is invalid.");

            return new Email(value.Trim().ToLower());
        }

        public override string ToString() => Value;
    }

}
