using ConnectPro.SharedKernel;
using Portfolio.Domain.Aggregates.Portfolio.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Aggregates.Portfolio.Entities
{
    public class ProfessionalInfo : Entity<ProfessionalInfo>
    {
        public Id<Portfolio> PortfolioId { get; private set; }
        private string _headline;
        private List<Skill> _skills;

        private ProfessionalInfo() { }

        private ProfessionalInfo(Guid portfolioId, string headline, List<Skill> skills)
        {
            Id = Guid.NewGuid();
            PortfolioId = portfolioId;
            _headline = headline;
            _skills = skills;
        }

        public static ProfessionalInfo Create(Guid portfolioId, string headline)
        {
            if (string.IsNullOrWhiteSpace(headline)) throw new ArgumentException("Headline is required.");
            if (headline.Length > 200) throw new ArgumentException("Headline must not exceed 200 characters.");

            return new ProfessionalInfo(portfolioId, headline.Trim(), new List<Skill>());
        }

        public void UpdateHeadline(string headline)
        {
            if (string.IsNullOrWhiteSpace(headline)) throw new ArgumentException("Headline is required.");
            if (headline.Length > 200) throw new ArgumentException("Headline must not exceed 200 characters.");
            _headline = headline.Trim();
        }

        public void AddSkill(Skill skill)
        {
            if (_skills.Any(s => s.Name == skill.Name))
                throw new InvalidOperationException($"Skill '{skill.Name}' already exists.");
            _skills.Add(skill);
        }

        public void ReplaceSkills(IReadOnlyList<Skill> skills)
        {
            var skillNames = new HashSet<string>();
            foreach (var skill in skills)
            {
                if (!skillNames.Add(skill.Name))
                    throw new InvalidOperationException($"Duplicate skill name '{skill.Name}' in the provided list.");
            }
            _skills = skills.ToList();
        }
        public void RemoveSkill(string skillName)
        {
            var skill = _skills.FirstOrDefault(s => s.Name == skillName);
            if (skill is null) throw new InvalidOperationException($"Skill '{skillName}' not found.");
            _skills.Remove(skill);
        }

        public string Headline => _headline;
        public IReadOnlyList<Skill> Skills => _skills.AsReadOnly();
    }
}
