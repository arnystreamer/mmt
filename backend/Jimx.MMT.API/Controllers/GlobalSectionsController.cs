using Jimx.MMT.API.App;
using Jimx.MMT.API.Context;
using Jimx.MMT.API.Models.Common;
using Jimx.MMT.API.Models.StaticItems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
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

		private readonly Expression<Func<Section, bool>> ExpressionIsSectionCategoryGlobal = 
			c => c.UserId == null && c.WalletId == null && c.SharedAccountId == null;

		public GlobalSectionsController(ILogger<GlobalSectionsController> logger, ApiDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet("{id}")]
		public GlobalSectionApi Get(int id)
		{
			var sectionDb = _context.Sections
				.Where(ExpressionIsSectionCategoryGlobal)
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
				.Where(ExpressionIsSectionCategoryGlobal);

			var sectionsTotalCount = sectionsAll.Count();

			int skip = requestApi.Skip ?? 0;
			int take = requestApi.Take ?? 10;
			var result = sectionsAll
				.Skip(skip).Take(take)
				.Select(s => s.ToGlobalSectionApi())
				.ToArray();

			return new CollectionApi<GlobalSectionApi>(sectionsTotalCount, skip, take, result.Length, result);
		}

		[HttpPost]
		public GlobalSectionApi Post(SectionEditApi sectionApi)
		{
			var section = new Section()
			{
				WalletId = null,
				SharedAccountId = null,
				UserId = null,

				Name = sectionApi.Name,
				Description = sectionApi.Description
			};

			var entity = _context.Sections.Add(section).Entity;

			_context.SaveChanges();

			return new GlobalSectionApi(section.Id, section.Name, section.Description);
		}

		[HttpPut]
		public GlobalSectionApi Put(SectionEditApi sectionApi)
		{
			var section = _context.Sections
				.Where(ExpressionIsSectionCategoryGlobal)
				.FirstOrDefault(s => s.Id == sectionApi.Id);

			if (section == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(sectionApi.Id), typeof(IdItem));
			}

			section.Name = sectionApi.Name;
			section.Description = sectionApi.Description;

			_context.SaveChanges();

			return new GlobalSectionApi(section.Id, section.Name, section.Description);
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var section = _context.Sections
				.Where(ExpressionIsSectionCategoryGlobal)
				.FirstOrDefault(s => s.Id == id);

			if (section == null)
			{
				return NotFound(new { Id = id });
			}

			_context.Sections.Remove(section);
			_context.SaveChanges();

			return NoContent();
		}
	}
}
