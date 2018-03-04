using System;
using Microsoft.IdentityModel.Tokens;

namespace BGTBackend.Middleware
{
    internal class TokenProviderOptions
    {
        public string Path { get; set; } = "/api/authentication";
        
        public string Issuer { get; set; }
        
        public string Audience { get; set; }

        public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(5);
        
        public SigningCredentials SigningCredentials { get; set; }
    }
}