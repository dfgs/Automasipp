using Automasipp.backend.DataSources;
using Microsoft.Extensions.Configuration;
using NReco.Logging.File;

namespace Automasipp.Backend
{
    public static class LoggerExtension
    {
        public static ILogger<T> CreateLogger<T>(this WebApplicationBuilder Builder)
        {
            return LoggerFactory.Create(config =>
            {
                config.AddConsole();
                config.AddConfiguration(Builder.Configuration.GetSection("Logging"));
                config.AddFile(Builder.Configuration.GetSection("Logging"));
            }).CreateLogger<T>();
        }
    }
}
