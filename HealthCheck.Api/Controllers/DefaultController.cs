using Microsoft.AspNetCore.Mvc;

namespace HealthCheck.Api.Controllers
{
    [ApiController]
    [Route("/")]
    public class DefaultController : ControllerBase
    {
        [HttpGet]
        [Route("/")]
        [Route("/health")]
        [Route("/healthcheck")]
        public IActionResult Index() => new RedirectResult("/dashboard");
    }
}