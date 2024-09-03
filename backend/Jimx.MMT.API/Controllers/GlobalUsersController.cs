using Jimx.MMT.API.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jimx.MMT.API.Controllers
{
	[ApiController]
	[Route("global-users")]
	[Authorize]
	public class GlobalUsersController : ControllerBase
	{
		private readonly ApiDbContext _context;
		private readonly ILogger<GlobalUsersController> _logger;

		public GlobalUsersController(ILogger<GlobalUsersController> logger, ApiDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			throw new NotImplementedException();
		}

		[HttpPost]
		public IActionResult Post()
		{
			throw new NotImplementedException();
		}

		[HttpPut]
		public IActionResult Put()
		{
			throw new NotImplementedException();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			throw new NotImplementedException();
		}
	}
}
