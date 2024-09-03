using Jimx.MMT.API.Context;
using Jimx.MMT.API.Models.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jimx.MMT.API.Controllers
{
	[ApiController]
	[Route("local-sections/{sectionId}/categories/")]
	[Authorize]
	public class LocalSectionsCategoriesController : ControllerBase
	{
		private readonly ApiDbContext _context;
		private readonly ILogger<LocalSectionsCategoriesController> _logger;

		public LocalSectionsCategoriesController(ILogger<LocalSectionsCategoriesController> logger, ApiDbContext context)
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

		[HttpPost]
		public IActionResult Post(int sectionId)
		{
			throw new NotImplementedException();
		}

		[HttpPut]
		public IActionResult Put(int sectionId)
		{
			throw new NotImplementedException();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int sectionId, int id)
		{
			throw new NotImplementedException();
		}
	}
	}
