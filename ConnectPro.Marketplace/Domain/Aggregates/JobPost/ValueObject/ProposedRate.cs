using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain.Aggregates.JobPost.ValueObject
{
    public sealed record ProposedRate
    {
        public decimal Amount { get; init; }

        private ProposedRate() { }

        public static ProposedRate Create(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Proposed rate must be greater than zero.", nameof(amount));

            return new ProposedRate { Amount = amount };
        }
    }
}
