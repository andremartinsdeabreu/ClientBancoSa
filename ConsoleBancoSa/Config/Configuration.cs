using ConsoleBancoSa.Client;
using ConsoleBancoSa.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace ConsoleBancoSa.Config
{
    public class Configuration
    {
        public static IConfigurationRoot ConfigBuild()
        {
            var build = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsetings.json", optional: false, reloadOnChange: true);

            return build.Build();
        }
        public static IHost HostBuild()
        {
            var builder = new HostBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                // with AddHttpClient we register the IHttpClientFactory
                services.AddHttpClient();
                services.AddTransient<ICreditoClient, CreditoClient>();
            }).UseConsoleLifetime();

            return builder.Build();
        }

    }
}
