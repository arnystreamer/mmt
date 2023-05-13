using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Jimx.MMX.API.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
	public class HealthController : ControllerBase
	{
		private readonly ILogger<HealthController> _logger;

		public HealthController(ILogger<HealthController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public IEnumerable<int> Get()
		{
			throw new NotImplementedException();
		}
	}
}