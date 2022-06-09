using HealthCheck.Api.Infra.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace HealthCheck.Api.Infra.IoC
{
    public static class AppServices
    {
        public static void RegisterHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            RegisterHttpClients(services, configuration);

            services.AddHealthChecks()
                .AddCheck<ManagerApiHealthCheck>("ManagerApi", tags: new List<string> {"api"}, timeout: TimeSpan.FromSeconds(30))
                .AddCheck<PromoApiHealthCheck>("PromoApi", tags: new List<string> { "api" }, timeout: TimeSpan.FromSeconds(30));
        }

        public static void RegisterHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<ManagerApiHealthCheck>(options =>
            {
                options.BaseAddress = new Uri(configuration["ManagerApi:HostUrl"]);
            });

            services.AddHttpClient<PromoApiHealthCheck>(options =>
            {
                options.BaseAddress = new Uri(configuration["PromoApi:HostUrl"]);
            });
        }
    }
}