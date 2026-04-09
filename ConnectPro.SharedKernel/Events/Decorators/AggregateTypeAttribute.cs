using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectPro.SharedKernel.Events.Decorators
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AggregateTypeAttribute(string aggregateType) : Attribute
    {
        public string AggregateType { get; } = aggregateType;
    }
}
