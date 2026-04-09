using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectPro.SharedKernel
{
    public interface IId : IComparable, IComparable<IId>, IComparable<Guid>, IEquatable<IId>
    {
        Guid Value { get; }
    }
}
