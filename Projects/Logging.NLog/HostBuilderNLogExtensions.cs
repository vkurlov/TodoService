using Microsoft.Extensions.Hosting;
using NLog.Web;

namespace Logging.NLog
{
    /// <summary>
    /// Provides extensions to register NLog
    /// </summary>
    public static class HostBuilderNLogExtensions
    {
        /// <summary>
        /// Activates NLog
        /// </summary>
        /// <param name="hostBuilder">See <see cref="IHostBuilder"/> description</param>
        /// <returns>See <see cref="IHostBuilder"/> description</returns>
        public static IHostBuilder UseNLogLogger(this IHostBuilder hostBuilder)
        {
            return hostBuilder.UseNLog();
        }
    }

}
