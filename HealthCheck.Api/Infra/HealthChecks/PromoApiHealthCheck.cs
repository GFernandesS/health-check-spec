using System.Net.Http;

namespace HealthCheck.Api.Infra.HealthChecks
{
    public class PromoApiHealthCheck : ApiBaseHealthCheck
    {
        public PromoApiHealthCheck(HttpClient httpClient) : base(httpClient)
        {}
    }
}