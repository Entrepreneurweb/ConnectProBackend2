using ConnectPro.SharedKernel;
using ConnectPro.SharedKernel.Exceptions;
using Portfolio.Domain.Aggregates.Portfolio.ValueObject;
using Portfolio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Aggregates.Portfolio.Entities
{


    public class Portfolio : AggregateRoot<Portfolio>
    {
        private Guid _ownerId;
        private PortfolioType _type;
        private PortfolioStatus _status;
        private int _activeServicesCount;

        private GeneralInfo? _generalInfo;
        private ContactInfo? _contactInfo;
        private LocationInfo? _locationInfo;
        private ProfessionalInfo? _professionalInfo;
        private List<Experience> _experiences = new();
        private List<Certification> _certifications = new();
        private List<SocialLink> _socialLinks = new();

        private Portfolio() { }

        private Portfolio(Guid ownerId, PortfolioType type)
        {
            _ownerId = ownerId;
            _type = type;
            _status = PortfolioStatus.Draft;
            _activeServicesCount = 0;
        }

        public static Portfolio Create(Guid ownerId, PortfolioType type)
        {
            return new Portfolio(ownerId, type);
        }

        //  Services

        public void IncrementActiveServices()
        {
            if (_activeServicesCount >= PortfolioLimits.MaxActiveServices(_type))
                throw new DomainException(" You've reached the max limit of active services of your portfolio type");
            _activeServicesCount++;
        }

        public void DecrementActiveServices()
        {
            if (_activeServicesCount == 0) return;
            _activeServicesCount--;
        }

        //  GeneralInfo 

        public void UpdateGeneralInfo(string firstName, string lastName, string? bio, string? avatarUrl)
        {
            if (_generalInfo is null)
                _generalInfo = GeneralInfo.Create(Id, firstName, lastName, bio, avatarUrl);
            else
            {
                _generalInfo.UpdateFirstName(firstName);
                _generalInfo.UpdateLastName(lastName);
                _generalInfo.UpdateBio(bio);
                _generalInfo.UpdateAvatarUrl(avatarUrl);
            }
        }

        //  ContactInfo 

        public void UpdateContactInfo(string email, string? phone, string? website)
        {
            if (_contactInfo is null)
                _contactInfo = ContactInfo.Create(Id, email, phone, website);
            else
            {
                _contactInfo.UpdateEmail(email);
                _contactInfo.UpdatePhone(phone);
                _contactInfo.UpdateWebsite(website);
            }
        }

        //  LocationInfo 

        public void UpdateLocationInfo(string country, string city, string? timezone)
        {
            if (_locationInfo is null)
                _locationInfo = LocationInfo.Create(Id, country, city, timezone);
            else
            {
                _locationInfo.UpdateCountry(country);
                _locationInfo.UpdateCity(city);
                _locationInfo.UpdateTimezone(timezone);
            }
        }

        //  ProfessionalInfo

        public void UpdateProfessionalInfo(string headline, IReadOnlyList<Skill> skills)
        {
            if (_professionalInfo is null)
            {
                _professionalInfo = ProfessionalInfo.Create(Id, headline);
                skills.ToList().ForEach(_professionalInfo.AddSkill);
            }
            else
            {
                _professionalInfo.UpdateHeadline(headline);
                _professionalInfo.ReplaceSkills(skills);
            }
        }

        //  SocialLinks

        public void AddSocialLink(string platform, string url)
        {
            if (_socialLinks.Any(s => s.Platform == platform))
                throw new InvalidOperationException($"Social link for '{platform}' already exists.");
            _socialLinks.Add(SocialLink.Create(platform, url));
        }

        public void RemoveSocialLink(string platform)
        {
            var link = _socialLinks.FirstOrDefault(s => s.Platform == platform);
            if (link is null) throw new InvalidOperationException($"Social link for '{platform}' not found.");
            _socialLinks.Remove(link);
        }

        //  Experiences 

        public Experience AddExperience(string company, string role, string? description, DateOnly start, DateOnly? end)
        {
            var experience = Experience.Create(Id, company, role, description, start, end);
            _experiences.Add(experience);
            return experience;
        }

        public void UpdateExperience(Guid experienceId, string company, string role, string? description, DateOnly start, DateOnly? end)
        {
            var experience = _experiences.FirstOrDefault(e => e.Id.Value == experienceId)
                ?? throw new InvalidOperationException($"Experience '{experienceId}' not found.");

            experience.UpdateCompany(company);
            experience.UpdateRole(role);
            experience.UpdateDescription(description);
            experience.UpdatePeriod(start, end);
        }

        public void RemoveExperience(Guid experienceId)
        {
            var experience = _experiences.FirstOrDefault(e => e.Id.Value == experienceId)
                ?? throw new InvalidOperationException($"Experience '{experienceId}' not found.");
            _experiences.Remove(experience);
        }

        // Certifications 

        public Certification AddCertification(string name, string issuingOrganization, DateOnly issueDate, DateOnly? expiryDate, string? credentialUrl)
        {
            var certification = Certification.Create(Id, name, issuingOrganization, issueDate, expiryDate, credentialUrl);
            _certifications.Add(certification);
            return certification;
        }

        public void UpdateCertification(Guid certificationId, string name, string issuingOrganization, DateOnly issueDate, DateOnly? expiryDate, string? credentialUrl)
        {
            var certification = _certifications.FirstOrDefault(c => c.Id.Value == certificationId)
                ?? throw new InvalidOperationException($"Certification '{certificationId}' not found.");

            certification.UpdateName(name);
            certification.UpdateIssuingOrganization(issuingOrganization);
            certification.UpdateIssueDate(issueDate);
            certification.UpdateExpiryDate(expiryDate);
            certification.UpdateCredentialUrl(credentialUrl);
        }

        public void RemoveCertification(Guid certificationId)
        {
            var certification = _certifications.FirstOrDefault(c => c.Id.Value == certificationId)
                ?? throw new InvalidOperationException($"Certification '{certificationId}' not found.");
            _certifications.Remove(certification);
        }

        // Getters  

        public Guid GetOwnerId => _ownerId;
        public PortfolioType GetPortfolioType => _type;
        public PortfolioStatus Status => _status;
        public int ActiveServicesCount => _activeServicesCount;
        public GeneralInfo? GeneralInfo => _generalInfo;
        public ContactInfo? ContactInfo => _contactInfo;
        public LocationInfo? LocationInfo => _locationInfo;
        public ProfessionalInfo? ProfessionalInfo => _professionalInfo;
        public IReadOnlyList<Experience> Experiences => _experiences.AsReadOnly();
        public IReadOnlyList<Certification> Certifications => _certifications.AsReadOnly();
        public IReadOnlyList<SocialLink> SocialLinks => _socialLinks.AsReadOnly();
    }



}
