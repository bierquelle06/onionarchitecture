using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Extensions
{
    public static class ServiceExtensions
    {
        private const string apiName = "Onion Architecture Web API";

        private const int apiMajorVersion = 1;
        private const int apiMinorVersion = 0;

        private const string apiEmailContact = "aykut@aykutaktas.net";
        private const string apiUrl = "https://www.aykutaktas.net";

        private const string apiXml = "OnionArchitecture.WebApi.xml";

        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                string xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\" + apiXml;

                c.IncludeXmlComments(xmlPath);
                c.SwaggerDoc("v" + apiMajorVersion, new OpenApiInfo
                {
                    Title = apiName,
                    Version = $"v{apiMajorVersion}.{apiMinorVersion}",
                    Description = $"{apiName} v{apiMajorVersion}.{apiMinorVersion} This Api will be responsible for overall data distribution and authorization.",
                    Contact = new OpenApiContact
                    {
                        Name = apiName,
                        Email = apiEmailContact,
                        Url = new Uri(apiUrl),
                    }
                });
            });
        }

        public static void AddApiVersioningExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                // Specify the default API Version as 1.0
                config.DefaultApiVersion = new ApiVersion(apiMajorVersion, apiMinorVersion);
                // If the client hasn't specified the API version in the request, use the default API version number 
                config.AssumeDefaultVersionWhenUnspecified = true;
                // Advertise the API versions supported for the particular endpoint
                config.ReportApiVersions = true;
            });
        }

        public static void UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v{apiMajorVersion}/swagger.json", $"{apiName} v{apiMajorVersion}.{apiMinorVersion}");
            });
        }

    }
}
