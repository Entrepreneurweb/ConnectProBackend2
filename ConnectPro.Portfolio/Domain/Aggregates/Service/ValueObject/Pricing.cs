using Portfolio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Aggregates.Service.ValueObject
{
    public record Pricing
    {
        public decimal Amount { get; }
        public Currency currency { get; }
        public PricingType Type { get; }

        private Pricing() { }

        private Pricing(decimal amount, Currency currency, PricingType type)
        {
            Amount = amount;
            this.currency = currency;
            Type = type;
        }

        public static Pricing Create(decimal amount, Currency currency, PricingType type)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.");
           if (!Enum.IsDefined(typeof(Currency), currency))
                throw new ArgumentException("Invalid currency.");

            return new Pricing(amount, currency, type);
        }

         public enum Currency
        {
            USD,
            EUR,
            GBP,
            XFA,
            TL
        }
    }
}
