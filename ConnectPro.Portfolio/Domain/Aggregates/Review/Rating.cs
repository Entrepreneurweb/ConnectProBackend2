using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Aggregates.Review
{
    public record Rating
    {
        public int Value { get; }

        private Rating() { }

        private Rating(int value)
        {
            Value = value;
        }

        public static Rating Create(int value)
        {
            if (value < 1 || value > 5)
                throw new ArgumentException("Rating must be between 1 and 5.");

            return new Rating(value);
        }

        public override string ToString() => Value.ToString();
    }

}
