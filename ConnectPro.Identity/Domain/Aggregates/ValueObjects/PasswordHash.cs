using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectPro.Identity.Domain.Aggregates.ValueObjects
{
    public record PasswordHash
    {
        public string Value { get; }
        public PasswordHash() { }
        public PasswordHash(string value)
        {
            Value = value;
        }
    }
}
