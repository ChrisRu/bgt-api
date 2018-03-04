using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using BGTBackend.Clients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BGTBackend.Middleware
{
    internal class TokenProviderMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly TokenProviderOptions _options;

        private readonly UserRepository _repository;

        public TokenProviderMiddleware(RequestDelegate next, IOptions<TokenProviderOptions> options)
        {
            this._next = next;
            this._options = options.Value;
            this._repository = new UserRepository();
        }

        public Task Invoke(HttpContext context)
        {
            // If path does not match /authentication, continue
            if (!context.Request.Path.Equals(this._options.Path, StringComparison.OrdinalIgnoreCase))
            {
                return this._next(context);
            }

            if (context.Request.Method != "POST")
            {
                context.Response.StatusCode = 405;
                return context.Response.WriteAsync("Use a post request to supply your login data");
            }

            if (!context.Request.HasFormContentType)
            {
                context.Response.StatusCode = 400;
                return context.Response.WriteAsync("Add your login data to the post request");
            }

            return this.GenerateToken(context);
        }
        
        [ValidateAntiForgeryToken]
        private async Task GenerateToken(HttpContext context)
        {
            var username = context.Request.Form["username"];
            var password = context.Request.Form["password"];

            var user = await this._repository.Get(new Dictionary<string, string>
            {
                {"username", username},
                {"password", password}
            });

            // TODO: OH BOY NICE COMPARISON BRO WHO NEEDS HASHING ANYWAYS AMIRITE
            bool result = password == user.Password;
            
            Console.WriteLine(password + " " + user.Password);

            if (!result)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid username or password");
                return;
            }

            var now = DateTime.UtcNow;

            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64),
            };

            var jwt = new JwtSecurityToken(
                this._options.Issuer,
                this._options.Audience,
                claims,
                now,
                now.Add(this._options.Expiration),
                this._options.SigningCredentials
            );

            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                token = encodedJwt,
                expires = (int) this._options.Expiration.TotalSeconds
            };

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}