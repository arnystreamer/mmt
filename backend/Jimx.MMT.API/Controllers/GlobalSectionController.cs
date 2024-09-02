using Jimx.MMT.API.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jimx.MMT.API.Controllers
{
	[ApiController]
	[Route("global-section")]
	[Authorize]
	public class GlobalSectionController : ControllerBase
	{
		private readonly ApiDbContext _context;
		private readonly ILogger<GlobalSectionController> _logger;

		public GlobalSectionController(ILogger<GlobalSectionController> logger, ApiDbContext context)
		{
			_logger = logger;
			_context = context;
		}
	}
}
