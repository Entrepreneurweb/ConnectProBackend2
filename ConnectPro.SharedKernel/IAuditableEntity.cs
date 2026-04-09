using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectPro.SharedKernel
{
    public class IAuditableEntity
    {
        public DateTimeOffset CreatedAtUtc { get; }
        public DateTimeOffset LastModifiedAtUtc { get; }
    }
}
