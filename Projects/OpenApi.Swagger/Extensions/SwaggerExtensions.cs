using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OpenApi.Swagger.Extensions
{
    /// <summary>
    /// Provides extensions to register Swagger API service
    /// </summary>
    public static class SwaggerExtensions
    {
        /// <summary>
        /// Contains registered api versions 
        /// </summary>
        private static string[] _apiVersions = new string[0];

        /// <summary>
        /// Assigns options for Swagger API
        /// </summary>
        /// <param name="services">See <see cref="IServiceCollection"/> documentation</param>
        /// <returns>See <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddSwaggerApi(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                foreach (var apiVersion in _apiVersions)
                {
                    options.SwaggerDoc(apiVersion, new OpenApiInfo
                    {
                        Version = apiVersion,
                        Title = "Todo API document",
                        Description = "Document to describe TODO service's API",
                        Contact = new OpenApiContact
                        {
                            Name = "velvetech",
                            Email = "<something>@velvetech.com",
                            Url = new Uri("https://velvetech.com")
                        }
                    });
                }

                AddXmlDocumentation(options);
            });
        }

        /// <summary>
        /// Registers Swagger and SwaggerUI middlewares
        /// </summary>
        /// <param name="app">See <see cref="IApplicationBuilder"/> documentation</param>
        /// <param name="apiVersionDescriptionProvider">See <see cref="IApiVersionDescriptionProvider"/> documentation</param>
        /// <returns>See <see cref="IApplicationBuilder"/></returns>
        public static IApplicationBuilder UseSwaggerApi(this IApplicationBuilder app, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            app.UseSwagger(options =>
            {
                options.RouteTemplate = "swagger-api/{documentName}/swagger.{json|yaml}";

                options.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    var servers = new List<OpenApiServer>
                    {
                        new OpenApiServer {Url = $"{httpReq.Scheme}://{httpReq.Host}"}
                    };

                    swagger.Servers = servers;

                });
            });
            

            _apiVersions = apiVersionDescriptionProvider.ApiVersionDescriptions.Select(description => description.ApiVersion.ToString()).ToArray();
            
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger-api";
                foreach (var apiVersion in _apiVersions)
                {
                    c.SwaggerEndpoint($"/swagger-api/{apiVersion}/swagger.json", $"TODO API v{apiVersion}");
                }
            });

            return app;
        }

        /// <summary>
        /// Adds XML files to generate swagger documents
        /// </summary>
        /// <param name="swaggerGenOptions">Swagger options</param>
        private static void AddXmlDocumentation(SwaggerGenOptions swaggerGenOptions)
        {
            var xmlFiles = GetXmlFilesOfComments(new []{ "microsoft.", "system." });
            if (xmlFiles == null)
                return;

            foreach (var file in xmlFiles)
            {
                if (File.Exists(file))
                    swaggerGenOptions.IncludeXmlComments(file);
            }
        }


        /// <summary>
        /// Gets XML comments file paths
        /// </summary>
        /// <returns><see cref="IEnumerable "/> XML comments file paths</returns>
        private static IEnumerable<string> GetXmlFilesOfComments(string[] doNotInclude = null)
        {
            var filePaths = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly);
            foreach (var filePath in filePaths)
            {
                if (doNotInclude != null && Array.Exists(doNotInclude, item => filePath.Contains(item)))
                {
                    continue;
                }

                yield return filePath;
            }
        }

    }
}