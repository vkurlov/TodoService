using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace OpenApi.Swagger.Extensions
{
    /// <summary>
    /// Provides extensions to register API versioning
    /// </summary>
    public static class ApiVersioningExtensions
    {
        /// <summary>
        /// Adds API versioning
        /// </summary>
        /// <param name="services">See <see cref="IServiceCollection"/></param>
        /// <param name="defaultVersion">Default API version</param>
        /// <returns>See <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddApiVersioning(this IServiceCollection services, ApiVersion defaultVersion)
        {
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = defaultVersion ?? ApiVersion.Parse("1.0");
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            return services;
        }
    }
}