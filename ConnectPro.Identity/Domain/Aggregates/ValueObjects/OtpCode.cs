using ConnectPro.SharedKernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

// ConnectPro.Identity.Domain.Aggregates.ValueObjects
namespace Identity.Domain.Aggregates.ValueObjects
{
    public record OtpCode
    {
        public string Value { get; }

        public OtpCode(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length != 4 || !value.All(char.IsDigit))
                throw new DomainException("L'OTP doit contenir exactement 4 chiffres.");

            Value = value;
        }

        public static OtpCode  Create(string value) => new OtpCode(value);

        public static OtpCode Generate()
        {
            var code = Random.Shared.Next(0, 9999).ToString("D4");
            return new OtpCode(code);
        }

        public override string ToString() => Value;
    }
}
