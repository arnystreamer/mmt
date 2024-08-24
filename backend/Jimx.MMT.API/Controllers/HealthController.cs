using Jimx.MMT.API.Context;
using Jimx.MMT.API.Models.Health;
using Microsoft.AspNetCore.Mvc;

namespace Jimx.MMT.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class HealthController : ControllerBase
	{
		private readonly ILogger<HealthController> _logger;
		private readonly IHostEnvironment _hostEnvironment;
		private readonly ApiDbContext _context;

		public HealthController(ILogger<HealthController> logger, IHostEnvironment hostEnvironment, ApiDbContext context)
		{
			_logger = logger;
			_hostEnvironment = hostEnvironment;
			_context = context;
		}

		[HttpGet]
		public CheckResult Get()
		{
			string databaseCreatedResultMessage;
			try
			{
				_ = _context.Categories.Count();
				databaseCreatedResultMessage = "Database OK";
			}
			catch(Exception ex)
			{
				databaseCreatedResultMessage = $"Fatal error {ex.GetType().Name}: {ex.Message}";
				_logger.LogWarning(ex, "GET Health");
			}

			return new CheckResult(_hostEnvironment.EnvironmentName, databaseCreatedResultMessage);
		}
	}
}