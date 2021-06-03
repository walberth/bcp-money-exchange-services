namespace MoneyExchange.Service.Api.Core
{
    using System;
    using System.IO;
    using System.Reflection;
    using Microsoft.OpenApi.Models;
    using Microsoft.AspNetCore.Builder;
    using Swashbuckle.AspNetCore.SwaggerUI;
    using Microsoft.Extensions.DependencyInjection;

    ///<Summary>
    /// Swagger extensions
    ///</Summary>
    public static class SwaggerServiceExtensions
    {

        ///<Summary>
        /// AddSwaggerDocumentation method
        ///</Summary>
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MoneyExchange API",
                    Description = "This is the swagger page for the MoneyExchange endpoints information",
                    Contact = new OpenApiContact
                    {
                        Name = "Walberth Gutierrez Telles",
                        Email = "w.felipe.gutierrez@gmail.com",
                        Url = new Uri("https://www.codingwithnotrycatch.com")
                    },
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    Scheme = "bearer",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
            });

            return services;
        }


        ///<Summary>
        /// UseSwaggerDocumentation method
        ///</Summary>
        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MoneyExchange API");

                c.RoutePrefix = string.Empty;
                c.DocumentTitle = "Swagger Exphadis API";
                c.DocExpansion(DocExpansion.None);
            });

            return app;
        }
    }
}
