using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using BGTBackend.Models;
using BGTBackend.Repositories;
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

        private readonly UserRepository _repository = new UserRepository();

        public TokenProviderMiddleware(RequestDelegate next, IOptions<TokenProviderOptions> options)
        {
            this._next = next;
            this._options = options.Value;
        }

        /// <summary>
        /// Intercept request to check if the path matches the authenticate route
        /// </summary>
        /// <param name="context">Context to send token to if path matches</param>
        /// <returns>Task that resolves when the request has been handled</returns>
        public Task Invoke(HttpContext context)
        {
            // If path does not match /authentication, continue
            if (!context.Request.Path.Equals(this._options.Path, StringComparison.OrdinalIgnoreCase))
            {
                return this._next(context);
            }

            if (context.Request.Method != "POST")
            {
                return Error(context, 405, "Gebruik een POST request om log in data mee te sturen");
            }

            return this.GenerateToken(context);
        }

        /// <summary>
        /// Create a JSON web token if credentials are correct
        /// </summary>
        /// <param name="context">Context to get data from and write response to</param>
        /// <returns>Task that resolves when JSON web token has been sent</returns>
        /// <exception cref="Exception">Exception thrown when data is incorrect</exception>
        [ValidateAntiForgeryToken]
        private async Task GenerateToken(HttpContext context)
        {
            string username;
            string password;

            try
            {
                using (StreamReader sr = new StreamReader(context.Request.Body))
                using (JsonTextReader jsonTextReader = new JsonTextReader(sr))
                {
                    dynamic response = new JsonSerializer().Deserialize(jsonTextReader);
                    username = response.username;
                    password = response.password;
                }
            }
            catch
            {
                await Error(context, 405, "Kan data niet lezen");
                return;
            }

            User user;
            try
            {
                user = this._repository.GetByUsername(username);

                if (user == null)
                    throw new Exception("No user exists with username: " + username);
            }
            catch (Exception error)
            {
                await Error(context, 401, "Gebruiker bestaat niet: " + error.Message);
                return;
            }

            // TODO: Compare passwords here
            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                await Error(context, 401, "Gebruikersnaam of wachtwoord is incorrect");
                return;
            }

            DateTime now = DateTime.UtcNow;

            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64)
            };

            JwtSecurityToken jwt = new JwtSecurityToken(
                this._options.Issuer,
                this._options.Audience,
                claims,
                now,
                now.Add(this._options.Expiration),
                this._options.SigningCredentials
            );

            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            await Send(context, new
            {
                access_token = encodedJwt,
                expires = (int) this._options.Expiration.TotalSeconds
            });
        }

        /// <summary>
        /// Write object in JSON to response
        /// </summary>
        /// <param name="context">Context to write to</param>
        /// <param name="message">Message to send</param>
        /// <returns>Task that completes when response message has been written</returns>
        private static Task Send(HttpContext context, object message)
        {
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(JsonConvert.SerializeObject(new Response(context.Response, message)));
        }

        /// <summary>
        /// Write error to response
        /// </summary>
        /// <param name="context">Context to write to</param>
        /// <param name="code">HTTP status error code</param>
        /// <param name="message">Message to send with error</param>
        /// <returns>Task thta completes when response error has been written</returns>
        private static Task Error(HttpContext context, int code, string message)
        {
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(
                JsonConvert.SerializeObject(new Response(context.Response, new Error(code, message))));
        }
    }
}