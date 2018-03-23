using System;
using Microsoft.IdentityModel.Tokens;

namespace BGTBackend.Middleware
{
    internal class TokenProviderOptions
    {
        public string Path { get; } = "/authenticate";

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public TimeSpan Expiration { get; } = TimeSpan.FromHours(6);

        public SigningCredentials SigningCredentials { get; set; }
    }
}