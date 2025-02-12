using Automasipp.backend.DataSources;

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
            }).CreateLogger<T>();
        }
    }
}
