using System;
using System.Text;
using BGTBackend.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BGTBackend
{
    internal class Startup
    {
        public static string ConnectionString { get; private set; }

        public static readonly MemoryCache MemoryCache = new MemoryCache(new MemoryCacheOptions());

        private SymmetricSecurityKey SigningKey { get; }

        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
            this.SigningKey =
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("Authentication")["Key"]));
            ConnectionString = configuration.GetSection("Database")["ConnectionString"];

            Console.WriteLine("Starting up the API");
        }

        /// This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver =
                    new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            IConfigurationSection auth = this.Configuration.GetSection("Authentication");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = this.SigningKey,

                        ValidateIssuer = true,
                        ValidIssuer = auth["Issuer"],

                        ValidateAudience = true,
                        ValidAudience = auth["Audience"],

                        ValidateLifetime = true,

                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddCors(
                o => o.AddPolicy("DefaultPolicy",
                    builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials()
                )
            );

            Console.WriteLine("Set up all services");
        }

        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            IConfigurationSection auth = this.Configuration.GetSection("Authentication");

            TokenProviderOptions jwtOptions = new TokenProviderOptions
            {
                Audience = auth["Audience"],
                Issuer = auth["Issuer"],
                SigningCredentials = new SigningCredentials(this.SigningKey, SecurityAlgorithms.HmacSha256)
            };

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePagesWithReExecute("/");
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseCors("DefaultPolicy");
            app.UseMvc();
            app.UseMiddleware<TokenProviderMiddleware>(Options.Create(jwtOptions));

            Console.WriteLine("Set up all app features");
        }
    }
}