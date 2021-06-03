namespace MoneyExchange
{
    using System;
    using System.Text;
    using Service.Api.Core;
    using Service.Api.Providers;
    using Service.Api.Middleware;
    using Microsoft.AspNetCore.Mvc;
    using FluentValidation.AspNetCore;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.Extensions.Logging.Debug;
    using Microsoft.Extensions.Configuration;
    using Infrastructure.Configuration.Context;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Authentication.JwtBearer;

    /// <summary>
    /// Startup the application
    /// </summary>
    public class Startup
    {
        private static readonly ILoggerFactory LoggerFactory = new LoggerFactory(new[] { new DebugLoggerProvider() });

        ///<Summary>
        /// Configuration static class of Startup
        ///</Summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configure services
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);

            services.AddCors(options => options.AddPolicy("AllowCors",
                builder =>
                {
                    builder
                        .AllowAnyHeader()
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetIsOriginAllowed(origin => true);
                }));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(config => {
                    config.TokenValidationParameters = TokenValidators();
                });

            services.AddEntityFrameworkSqlite()
                .AddDbContext<MoneyExchangeContext>(x => x.UseLoggerFactory(LoggerFactory)
                .EnableSensitiveDataLogging()
                .UseSqlite(Configuration.GetConnectionString("conn"))
                /*.UseInMemoryDatabase("bcp")*/);

            services.AddMvcCore()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddFluentValidation()
                .AddApiExplorer();

            //services.AddControllers()
            //    .AddNewtonsoftJson(options => {
            //        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //    });

            services.AddControllers();

            services.AddOptions();
            services.AddSwaggerDocumentation();
            services.ConfigureServiceCollection();
        }

        /// <summary>
        /// Configure the startup app
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowCors");

            app.UseAuthentication();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHttpsRedirection();

            app.UseSwaggerDocumentation();
        }

        /// <summary>
        /// Validate the token params
        /// </summary>
        /// <returns></returns>
        protected TokenValidationParameters TokenValidators()
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = Configuration["JWT:Issuer"],
                ValidAudience = Configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY") ?? throw new InvalidOperationException()))
            };
        }
    }
}
