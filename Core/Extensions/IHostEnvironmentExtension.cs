using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Core.Extensions.IHostEnvironmentExtension
{
    public static class IHostEnvironmentExtension
    {
        public static readonly string ConfigurationNameSeperator = ".";

        /// <summary>
        /// Load configuration files by given environment name.
        /// </summary>
        public static IConfigurationRoot BuildConfiguration(this IHostEnvironment hostEnvironment)
        {
            if (String.IsNullOrWhiteSpace(hostEnvironment.EnvironmentName))
            {
                throw new ArgumentNullException("Environment name is not set.");
            }

            var environmentNames = hostEnvironment.EnvironmentName.Split(ConfigurationNameSeperator);

            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            for (var i = 1; i <= environmentNames.Length; i++)
            {
                var configurationName = String.Join(ConfigurationNameSeperator, environmentNames.Take(i));
                builder = builder.AddJsonFile($"appsettings.{configurationName}.json", optional: true, reloadOnChange: true);
            }

            builder = builder.AddEnvironmentVariables();
            return builder.Build();
        }

        /// <summary>
        /// Check if environment is development environment.
        /// </summary>
        public static bool IsDevelopmentBase(this IHostEnvironment hostEnvironment)
        {
            var environmentName = hostEnvironment.EnvironmentName.ToLower().Split(ConfigurationNameSeperator).FirstOrDefault();
            return environmentName == "development";
        }
    }
}