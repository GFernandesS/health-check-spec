using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HealthCheck.Api.Infra.HealthChecks
{
    public abstract class ApiBaseHealthCheck : IHealthCheck
    {
        protected readonly HttpClient httpClient;
        protected const string API_UNAVAILABLE_MESSAGE = "Api indisponível! :(";
        protected const string API_AVAILABLE_MESSAGE = "Api disponível! :)";
        protected readonly string healthPath;

        public ApiBaseHealthCheck(HttpClient httpClient, string healthPath = "/health")
        {
            this.httpClient = httpClient;
            this.healthPath = healthPath;
        }

        public virtual async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await httpClient.GetAsync(healthPath, cancellationToken);

                var responseMessage = await response.Content.ReadAsStringAsync(cancellationToken);

                if (!response.IsSuccessStatusCode || responseMessage.Contains("Unhealthy"))
                    return HealthCheckResult.Unhealthy(API_UNAVAILABLE_MESSAGE, data: new Dictionary<string, object> { { "statusCode", response.StatusCode } });

                return HealthCheckResult.Healthy(API_AVAILABLE_MESSAGE, data: new Dictionary<string, object> { { "statusCode", response.StatusCode } });
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy(API_UNAVAILABLE_MESSAGE, exception: ex);
            }
        }
    }
}