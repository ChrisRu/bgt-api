using System;
using System.Text;
using BGTBackend.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BGTBackend
{
    internal class Startup
    {
        // TODO: FIX THIS WITH ENV VARIABLES
        private const string SecretKey = "temporary_super_secret_key_lol_hahahahahahahahahaha";
        private const string Issuer = "TestUser";
        private const string Audience = "TestAudience";
        private SymmetricSecurityKey SigningKey { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
            this.SigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

            Console.WriteLine("Started");
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = this.SigningKey,

                    ValidateIssuer = true,
                    ValidIssuer = Issuer,

                    ValidateAudience = true,
                    ValidAudience = Audience,

                    ValidateLifetime = true,

                    ClockSkew = TimeSpan.Zero
                };
            });
            
            services.AddCors(o => o.AddPolicy("DefaultPolicy",
                builder => { builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials(); }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var jwtOptions = new TokenProviderOptions
            {
                Audience = Audience,
                Issuer = Issuer,
                SigningCredentials = new SigningCredentials(this.SigningKey, SecurityAlgorithms.HmacSha256)
            };

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseMvc();
            app.UseMiddleware<TokenProviderMiddleware>(Options.Create(jwtOptions));
            app.UseCors("DefaultPolicy");
            // app.UseFileServer();
        }
    }
}