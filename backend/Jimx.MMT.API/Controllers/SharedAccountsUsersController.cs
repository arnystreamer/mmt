using Jimx.MMT.API.Context;
using Jimx.MMT.API.Models.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jimx.MMT.API.Controllers
{
	[ApiController]
	[Route("shared-accounts/{accountId}/users/")]
	[Authorize]
	public class SharedAccountsUsersController : ControllerBase
	{
		private readonly ApiDbContext _context;
		private readonly ILogger<SharedAccountsUsersController> _logger;

		public SharedAccountsUsersController(ILogger<SharedAccountsUsersController> logger, ApiDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet("{id}")]
		public IActionResult Get(int accountId, int id)
		{
			throw new NotImplementedException();
		}

		[HttpGet]
		public IActionResult GetAll(int accountId, [FromQuery] CollectionRequestApi requestApi)
		{
			throw new NotImplementedException();
		}

		[HttpPost]
		public IActionResult Post(int accountId)
		{
			throw new NotImplementedException();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int accountId, int id)
		{
			throw new NotImplementedException();
		}
	}
}
