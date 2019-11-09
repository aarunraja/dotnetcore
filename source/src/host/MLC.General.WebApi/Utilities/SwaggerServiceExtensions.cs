using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MLC.General.WebApi
{
    /// <summary>
    /// SwaggerServiceExtensions
    /// </summary>
    public static class SwaggerServiceExtensions
    {
        /// <summary>
        /// Add Swagger
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Version = "v1",
                    Title = "CMS API",
                    Description = "CMS First ASP.NET Core Web API",
                    TermsOfService = "None"
                });

                var app = System.AppContext.BaseDirectory;
                c.IncludeXmlComments(System.IO.Path.Combine(app, "MLC.General.WebApi.xml"));
                c.DescribeAllEnumsAsStrings();

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(security);


            });

            return services;

        }

        /// <summary>
        /// UseSwaggerDocumentation
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("~/swagger/v1.0/swagger.json", "Versioned API v1.0");
            });

            return app;
        }

    }
}
