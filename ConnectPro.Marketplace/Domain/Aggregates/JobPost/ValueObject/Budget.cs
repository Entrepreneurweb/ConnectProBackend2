using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain.Aggregates.JobPost.ValueObject
{
    public sealed record Budget
    {
        public decimal Amount { get; init; }

        private Budget() { }

        public static Budget Create(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Budget must be greater than zero.", nameof(amount));

            return new Budget { Amount = amount };
        }
    }
}
