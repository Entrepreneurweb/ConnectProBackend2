using ConnectPro.SharedKernel;
using Portfolio.Domain.Aggregates.Portfolio.Entities;
using Portfolio.Domain.Aggregates.Portfolio.ValueObject;
using Portfolio.Domain.Aggregates.Service.Events;
using Portfolio.Domain.Aggregates.Service.ValueObject;
using Portfolio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Portfolio.Domain.Aggregates.Service.ValueObject.Pricing;

namespace Portfolio.Domain.Aggregates.Service.Entities
{
    public class Service : AggregateRoot<Service>
    {
        private Guid _portfolioId;
        private string _title;
        private string _description;
        private ServiceStatus _status;
        private Url? _coverImageUrl;

        private List<Tag> _tags = new();
        private List<ImageUrl> _imageUrls = new();
        private Pricing? _pricing;
        private List<FAQ> _faqs = new();
        private List<Award> _awards = new();

        private Service() { }

        private Service(Guid portfolioId, string title, string description)
        {
            _portfolioId = portfolioId;
            _title = title;
            _description = description;
            _status = ServiceStatus.Draft;
        }

        public static Service Create(Guid portfolioId, string title, string description)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title is required.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description is required.");
            if (title.Length > 200) throw new ArgumentException("Title must not exceed 200 characters.");

            return new Service(portfolioId, title.Trim(), description.Trim());
        }

        // Content

        public void UpdateContent(string title, string description)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title is required.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description is required.");
            _title = title.Trim();
            _description = description.Trim();
        }

        public void UpdateCoverImage(string url) => _coverImageUrl = Url.Create(url);

        // Status 

        public void Publish()
        {
            if (_status == ServiceStatus.Published)
                throw new InvalidOperationException("Service is already published.");
            if (_pricing is null)
                throw new InvalidOperationException("Cannot publish a service without pricing.");
            _status = ServiceStatus.Published;
            this.RaiseDomainEvent(new ServiceCreatedEvent(this.Id.Value ,  this.Id.Value, this._title, this._description));

        }

        public void Pause()
        {
            if (_status != ServiceStatus.Published)
                throw new InvalidOperationException("Only published services can be paused.");
            _status = ServiceStatus.Paused;
        }

        // Tags 

        public Tag AddTag(string value)
        {
            if (_tags.Any(t => t.Value == value.Trim().ToLower()))
                throw new InvalidOperationException($"Tag '{value}' already exists.");
            if (_tags.Count >= 10)
                throw new InvalidOperationException("A service cannot have more than 10 tags.");

            var tag = Tag.Create(Id, value);
            _tags.Add(tag);
            return tag;
        }

        public void RemoveTag(Guid tagId)
        {
            var tag = _tags.FirstOrDefault(t => t.Id.Value == tagId)
                ?? throw new InvalidOperationException($"Tag '{tagId}' not found.");
            _tags.Remove(tag);
        }

        // Images 

        public ImageUrl AddImage(string url)
        {
            if (_imageUrls.Any(i => i.Value.Value == url.Trim()))
                throw new InvalidOperationException($"Image '{url}' already exists.");
            if (_imageUrls.Count >= 10)
                throw new InvalidOperationException("A service cannot have more than 10 images.");

            var image = ImageUrl.Create(Id, url);
            _imageUrls.Add(image);
            return image;
        }

        public void RemoveImage(Guid imageId)
        {
            var image = _imageUrls.FirstOrDefault(i => i.Id.Value == imageId)
                ?? throw new InvalidOperationException($"Image '{imageId}' not found.");
            _imageUrls.Remove(image);
        }

        // Pricing

        public void UpdatePricing(decimal amount, Currency currency, PricingType type) =>
            _pricing = Pricing.Create(amount, currency, type);

        //  FAQs 

        public FAQ AddFaq(string question, string answer)
        {
            var faq = FAQ.Create(Id, question, answer);
            _faqs.Add(faq);
            return faq;
        }

        public void UpdateFaq(Guid faqId, string question, string answer)
        {
            var faq = _faqs.FirstOrDefault(f => f.Id.Value == faqId)
                ?? throw new InvalidOperationException($"FAQ '{faqId}' not found.");
            faq.UpdateQuestion(question);
            faq.UpdateAnswer(answer);
        }

        public void RemoveFaq(Guid faqId)
        {
            var faq = _faqs.FirstOrDefault(f => f.Id.Value == faqId)
                ?? throw new InvalidOperationException($"FAQ '{faqId}' not found.");
            _faqs.Remove(faq);
        }

        //  Awards 

        public Award AddAward(string title, string issuedBy, DateOnly issuedAt, string? description)
        {
            var award = Award.Create(Id, title, issuedBy, issuedAt, description);
            _awards.Add(award);
            return award;
        }

        public void UpdateAward(Guid awardId, string title, string issuedBy, DateOnly issuedAt, string? description)
        {
            var award = _awards.FirstOrDefault(a => a.Id.Value == awardId)
                ?? throw new InvalidOperationException($"Award '{awardId}' not found.");
            award.UpdateTitle(title);
            award.UpdateIssuedBy(issuedBy);
            award.UpdateIssuedAt(issuedAt);
            award.UpdateDescription(description);
        }

        public void RemoveAward(Guid awardId)
        {
            var award = _awards.FirstOrDefault(a => a.Id.Value == awardId)
                ?? throw new InvalidOperationException($"Award '{awardId}' not found.");
            _awards.Remove(award);
        }

        //  Getters 

        public Guid PortfolioId => _portfolioId;
        public string GetTitle => _title;
        public string GetDescription => _description;
        public ServiceStatus Status => _status;
        public Url? CoverImageUrl => _coverImageUrl;
        public Pricing? Pricing => _pricing;
        public IReadOnlyList<Tag> Tags => _tags.AsReadOnly();
        public IReadOnlyList<ImageUrl> ImageUrls => _imageUrls.AsReadOnly();
        public IReadOnlyList<FAQ> Faqs => _faqs.AsReadOnly();
        public IReadOnlyList<Award> Awards => _awards.AsReadOnly();
    }
}
