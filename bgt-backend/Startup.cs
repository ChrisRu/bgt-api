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
using Newtonsoft.Json.Serialization;

namespace BGTBackend
{
    internal class Startup
    {
        private SymmetricSecurityKey SigningKey { get; }

        public static string ConnectionString { get; set; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
            this.SigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("Authentication")["Key"]));
            ConnectionString = configuration.GetSection("Database")["ConnectionString"];

            Console.WriteLine("Starting up the API");
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver =
                    new CamelCasePropertyNamesContractResolver();
            });

            IConfigurationSection auth = this.Configuration.GetSection("Authentication");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            IConfigurationSection auth = this.Configuration.GetSection("Authentication");

            var jwtOptions = new TokenProviderOptions
            {
                Audience = auth["Audience"],
                Issuer = auth["Issuer"],
                SigningCredentials = new SigningCredentials(this.SigningKey, SecurityAlgorithms.HmacSha256)
            };

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseCors("DefaultPolicy");
            app.UseMvc();
            app.UseMiddleware<TokenProviderMiddleware>(Options.Create(jwtOptions));

            Console.WriteLine("Set up all app features");
        }
    }
}