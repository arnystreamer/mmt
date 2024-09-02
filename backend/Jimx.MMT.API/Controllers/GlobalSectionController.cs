using Jimx.MMT.API.Context;
using Jimx.MMT.API.Models.Common;
using Jimx.MMT.API.Models.StaticItems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

		[HttpGet("{id}")]
		public GlobalSectionApi Get(int id)
		{
			_context.Sections
				.Where(s => s.UserId == null && s.WalletId == null && s.SharedAccountId == null)
				.FirstOrDefault(s => s.Id == id);

			throw new NotImplementedException();
		}

		[HttpGet]
		public CollectionApi<GlobalSectionApi> GetAll([FromQuery] CollectionRequestApi requestApi)
		{
			_context.Sections
				.Where(s => s.UserId == null && s.WalletId == null && s.SharedAccountId == null);

			throw new NotImplementedException();
		}

		[HttpPost]
		public GlobalSectionApi Post(GlobalSectionApi sectionApi)
		{
			throw new NotImplementedException();
		}

		[HttpPut]
		public GlobalSectionApi Put(GlobalSectionApi sectionApi)
		{
			_context.Sections
				.Where(s => s.UserId == null && s.WalletId == null && s.SharedAccountId == null)
				.FirstOrDefault(s => s.Id == sectionApi.Id);

			throw new NotImplementedException();
		}

		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			_context.Sections
				.Where(s => s.UserId == null && s.WalletId == null && s.SharedAccountId == null)
				.FirstOrDefault(s => s.Id == id);

			throw new NotImplementedException();
		}

		[HttpGet("{id}/categories")]
		public GlobalSectionCategoriesApi GetCategories(int id)
		{
			_context.Sections
				.Include(s => s.Categories)
				.Where(s => s.UserId == null && s.WalletId == null && s.SharedAccountId == null)
				.FirstOrDefault(s => s.Id == id);

			throw new NotImplementedException();
		}
	}
}
