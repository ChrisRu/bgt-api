using System;
using Microsoft.IdentityModel.Tokens;

namespace BGTBackend.Middleware
{
    internal class TokenProviderOptions
    {
        public string Path { get; } = "/api/authenticate";

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public TimeSpan Expiration { get; } = TimeSpan.FromMinutes(5);

        public SigningCredentials SigningCredentials { get; set; }
    }
}