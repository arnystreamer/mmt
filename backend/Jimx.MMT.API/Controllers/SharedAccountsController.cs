using Jimx.MMT.API.Context;
using Jimx.MMT.API.Models.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jimx.MMT.API.Controllers
{
	[ApiController]
	[Route("shared-accounts")]
	[Authorize]
	public class SharedAccountsController : ControllerBase
	{
		private readonly ApiDbContext _context;
		private readonly ILogger<SharedAccountsController> _logger;

		public SharedAccountsController(ILogger<SharedAccountsController> logger, ApiDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			throw new NotImplementedException();
		}

		[HttpGet]
		public IActionResult GetAll([FromQuery] CollectionRequestApi requestApi)
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
