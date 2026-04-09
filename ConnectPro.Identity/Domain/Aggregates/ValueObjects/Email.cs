using ConnectPro.SharedKernel.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace Identity.Domain.Aggregates.ValueObjects
{
  public record Email
    {
        public string Value { get; }

        public Email() { }
        public Email(string value)
        {
            Value = value.EnsureValidEmail( nameof(value) ) ;
        }
    }
}
