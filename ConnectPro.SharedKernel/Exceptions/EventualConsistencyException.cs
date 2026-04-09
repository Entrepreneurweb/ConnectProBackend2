using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectPro.SharedKernel.Exceptions
{
    public class EventualConsistencyException : Exception
    {
        public string ErrorCode { get; }
        public string ErrorMessage { get; }
        public List<string> Details { get; }

        public EventualConsistencyException(string errorCode, string errorMessage, List<string>? details = null)
            : base(message: errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            Details = details ?? new();
        }
    }
}
