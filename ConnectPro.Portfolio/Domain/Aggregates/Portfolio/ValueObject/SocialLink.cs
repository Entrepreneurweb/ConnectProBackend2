using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Aggregates.Portfolio.ValueObject
{
    public record SocialLink
    {
        public string Platform { get; }
        public Url Url { get; }

        private SocialLink() { }

        private SocialLink(string platform, Url url)
        {
            Platform = platform;
            Url = url;
        }

        public static SocialLink Create(string platform, string url)
        {
            if (string.IsNullOrWhiteSpace(platform))
                throw new ArgumentException("Platform is required.");

            return new SocialLink(platform.Trim(), Url.Create(url));
        }
    }
}
