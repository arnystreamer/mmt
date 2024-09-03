using Jimx.MMT.API.App;
using Jimx.MMT.API.Context;
using Jimx.MMT.API.Models.Common;
using Jimx.MMT.API.Models.StaticItems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Jimx.MMT.API.Controllers
{
	[ApiController]
	[Route("global-sections")]
	[Authorize]
	public class GlobalSectionsController : ControllerBase
	{
		private readonly ApiDbContext _context;
		private readonly ILogger<GlobalSectionsController> _logger;

		public GlobalSectionsController(ILogger<GlobalSectionsController> logger, ApiDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet("{id}")]
		public GlobalSectionApi Get(int id)
		{
			var sectionDb = _context.Sections
				.Where(s => s.UserId == null && s.WalletId == null && s.SharedAccountId == null)
				.FirstOrDefault(s => s.Id == id);

			if (sectionDb == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return new GlobalSectionApi(sectionDb.Id, sectionDb.Name, sectionDb.Description);
		}

		[HttpGet]
		public CollectionApi<GlobalSectionApi> GetAll([FromQuery] CollectionRequestApi requestApi)
		{
			var sectionsAll = _context.Sections
				.Where(s => s.UserId == null && s.WalletId == null && s.SharedAccountId == null);

			var sectionsTotal = sectionsAll.Count();

			int skip = requestApi.Skip ?? 0;
			int take = requestApi.Take ?? 10;
			var sectionsDb = sectionsAll.Skip(skip).Take(take).ToArray();

			return new CollectionApi<GlobalSectionApi>(sectionsDb.Length, skip, take, sectionsDb.Length,
				sectionsDb.Select(s => new GlobalSectionApi(s.Id, s.Name, s.Description)).ToArray());
		}

		[HttpPost]
		public GlobalSectionApi Post(GlobalSectionApi sectionApi)
		{
			var sectionDb = new Section()
			{
				WalletId = null,
				SharedAccountId = null,
				UserId = null,

				Name = sectionApi.Name,
				Description = sectionApi.Description
			};

			_context.Sections.Add(sectionDb);

			_context.SaveChanges();

			return new GlobalSectionApi(sectionDb.Id, sectionDb.Name, sectionDb.Description);
		}

		[HttpPut]
		public GlobalSectionApi Put(GlobalSectionApi sectionApi)
		{
			var sectionDb = _context.Sections
				.Where(s => s.UserId == null && s.WalletId == null && s.SharedAccountId == null)
				.FirstOrDefault(s => s.Id == sectionApi.Id);

			if (sectionDb == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(sectionApi.Id), typeof(IdItem));
			}

			sectionDb.Name = sectionApi.Name;
			sectionDb.Description = sectionApi.Description;

			_context.SaveChanges();

			return new GlobalSectionApi(sectionDb.Id, sectionDb.Name, sectionDb.Description);
		}

		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			var sectionDb = _context.Sections
				.Where(s => s.UserId == null && s.WalletId == null && s.SharedAccountId == null)
				.FirstOrDefault(s => s.Id == id);

			if (sectionDb == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			_context.Sections.Remove(sectionDb);
			_context.SaveChanges();
		}

		[HttpGet("{id}/categories")]
		[Obsolete]
		public GlobalSectionCategoriesApi GetCategories(int id)
		{
			var sectionDb = _context.Sections
				.Include(s => s.Categories)
				.Where(s => s.UserId == null && s.WalletId == null && s.SharedAccountId == null)
				.FirstOrDefault(s => s.Id == id);

			if (sectionDb == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return new GlobalSectionCategoriesApi(sectionDb.Id, sectionDb.Name, sectionDb.Description,
				sectionDb.Categories.Select(c => new CategoryApi(c.Id, sectionDb.Id, c.Name, c.Description)).ToArray());
		}
	}
}
