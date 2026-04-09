using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Aggregates.Portfolio.ValueObject
{
    public record Skill
    {
        public string Name { get; }
        public string Level { get; }

        private static readonly string[] ValidLevels = { "Beginner", "Intermediate", "Expert" };

        private Skill() { }

        private Skill(string name, string level)
        {
            Name = name;
            Level = level;
        }

        public static Skill Create(string name, string level)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Skill name is required.");
            if (!ValidLevels.Contains(level))
                throw new ArgumentException($"Level must be one of: {string.Join(", ", ValidLevels)}.");

            return new Skill(name.Trim(), level);
        }
    }
}
