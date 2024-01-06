using Jimx.MMT.API.Models.Health;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Jimx.MMT.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class HealthController : ControllerBase
	{
		private readonly ILogger<HealthController> _logger;
		private readonly IHostEnvironment _hostEnvironment;

		public HealthController(ILogger<HealthController> logger, IHostEnvironment hostEnvironment)
		{
			_logger = logger;
			_hostEnvironment = hostEnvironment;
		}

		[HttpGet]
		public CheckResult Get()
		{
			return new CheckResult(_hostEnvironment.EnvironmentName);
		}
	}
}