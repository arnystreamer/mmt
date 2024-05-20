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
				var isDatabaseCreated = _context.Database.EnsureCreated();
				databaseCreatedResultMessage = isDatabaseCreated ? "Database OK" : "Error: database not exists";
			}
			catch(Exception ex)
			{
				databaseCreatedResultMessage = $"Fatal error {ex.GetType().Name}: {ex.Message}";
			}

			return new CheckResult(_hostEnvironment.EnvironmentName, databaseCreatedResultMessage);
		}
	}
}