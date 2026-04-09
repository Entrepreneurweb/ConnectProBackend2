using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application
{
    public abstract class Command
    {
        CancellationToken CancellationToken { get; }
    }
}
