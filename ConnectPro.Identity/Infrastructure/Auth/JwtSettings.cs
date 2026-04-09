using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectPro.Identity.Infrastructure.Auth
{
    public sealed class JwtSettings
    {
        public const string SectionName = "JwtSettings";

        public string SecretKey { get; init; } = default!;
        public string Issuer { get; init; } = default!;
        public string Audience { get; init; } = default!;
        public int ExpirationMinutes { get; init; } = 60;
    }
}
