using System.Net.Http;

namespace HealthCheck.Api.Infra.HealthChecks
{
    public class ManagerApiHealthCheck : ApiBaseHealthCheck
    {
        public ManagerApiHealthCheck(HttpClient httpClient) : base(httpClient)
        {}
    }
}