using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Aggregates.Portfolio.ValueObject
{
    public record DateRange
    {
        public DateOnly Start { get; }
        public DateOnly? End { get; }

        private DateRange() { }

        private DateRange(DateOnly start, DateOnly? end)
        {
            Start = start;
            End = end;
        }

        public static DateRange Create(DateOnly start, DateOnly? end)
        {
            if (end.HasValue && end.Value < start)
                throw new ArgumentException("End date must be after start date.");

            return new DateRange(start, end);
        }

        public bool IsCurrent => !End.HasValue;
    }
}
