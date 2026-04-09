using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectPro.SharedKernel.Exceptions
{
    public class DomainException : Exception
    {
        public string? ErrorCode { get; }
        public string ErrorMessage { get; }
        public List<string>? Details { get; }

        public DomainException(string errorCode, string errorMessage, List<string>? details = null)
            : base(errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            Details = details ?? new List<string>();
        }
        public DomainException(  string errorMessage)
            : base(errorMessage)
        {
            
            ErrorMessage = errorMessage;
          
        }
    }
}
