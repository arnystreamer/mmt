using Jimx.MMT.API.Context;
using Jimx.MMT.API.Models.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jimx.MMT.API.Controllers
{
	[ApiController]
	[Route("sections")]
	[Authorize]
	public class SectionsController : ControllerBase
	{
		private readonly ApiDbContext _context;
		private readonly ILogger<SectionsController> _logger;

		public SectionsController(ILogger<SectionsController> logger, ApiDbContext context)
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
	}
}
