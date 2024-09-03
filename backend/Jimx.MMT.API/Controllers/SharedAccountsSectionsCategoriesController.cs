using Jimx.MMT.API.Context;
using Jimx.MMT.API.Models.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jimx.MMT.API.Controllers
{
	[ApiController]
	[Route("shared-accounts/{accountId}/sections/{sectionId}/categories/")]
	[Authorize]
	public class SharedAccountsSectionsCategoriesController : ControllerBase
	{
		private readonly ApiDbContext _context;
		private readonly ILogger<SharedAccountsSectionsCategoriesController> _logger;

		public SharedAccountsSectionsCategoriesController(ILogger<SharedAccountsSectionsCategoriesController> logger, ApiDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet("{id}")]
		public IActionResult Get(int accountId, int sectionId, int id)
		{
			throw new NotImplementedException();
		}

		[HttpGet]
		public IActionResult GetAll(int accountId, int sectionId, [FromQuery] CollectionRequestApi requestApi)
		{
			throw new NotImplementedException();
		}

		[HttpPost]
		public IActionResult Post(int accountId, int sectionId)
		{
			throw new NotImplementedException();
		}

		[HttpPut]
		public IActionResult Put(int accountId, int sectionId)
		{
			throw new NotImplementedException();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int accountId, int sectionId, int id)
		{
			throw new NotImplementedException();
		}
	}
}
