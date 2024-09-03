using Jimx.MMT.API.Context;
using Jimx.MMT.API.Models.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jimx.MMT.API.Controllers
{
	[ApiController]
	[Route("sections/{sectionId}/categories/")]
	[Authorize]
	public class SectionsCategoriesController : ControllerBase
	{
		private readonly ApiDbContext _context;
		private readonly ILogger<SectionsCategoriesController> _logger;

		public SectionsCategoriesController(ILogger<SectionsCategoriesController> logger, ApiDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet("{id}")]
		public IActionResult Get(int sectionId, int id)
		{
			throw new NotImplementedException();
		}

		[HttpGet]
		public IActionResult GetAll(int sectionId, [FromQuery] CollectionRequestApi requestApi)
		{
			throw new NotImplementedException();
		}
	}
	}
