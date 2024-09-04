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
	[Route("global-sections/{sectionId}/categories/")]
	[Authorize]
	public class GlobalSectionsCategoriesController : ControllerBase
	{
		private readonly ApiDbContext _context;
		private readonly ILogger<GlobalSectionsCategoriesController> _logger;

		private readonly Func<int, Expression<Func<Category, bool>>> ExpressionIsSectionCategoryGlobal = sectionId =>
			c => c.Section.UserId == null && c.Section.WalletId == null && c.Section.SharedAccountId == null && c.Section.Id == sectionId;

		private readonly Expression<Func<Section, bool>> ExpressionIsSectionGlobal =
			s => s.UserId == null && s.WalletId == null && s.SharedAccountId == null;

		public GlobalSectionsCategoriesController(ILogger<GlobalSectionsCategoriesController> logger, ApiDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet("{id}")]
		public CategoryApi Get(int sectionId, int id)
		{
			var category = _context.Categories
				.Where(ExpressionIsSectionCategoryGlobal(sectionId))
				.FirstOrDefault(c => c.Id == id);

			if (category == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return category.ToCategoryApi();
		}

		[HttpGet]
		public CollectionApi<CategoryApi> GetAll(int sectionId, [FromQuery] CollectionRequestApi requestApi)
		{
			var categoriesTotalCount = _context.Categories
				.Where(ExpressionIsSectionCategoryGlobal(sectionId))
				.Count();

			int skip = requestApi.Skip ?? 0;
			int take = requestApi.Take ?? 10;
			var result = _context.Categories
				.Where(ExpressionIsSectionCategoryGlobal(sectionId))
				.Skip(skip).Take(take).Select(c => c.ToCategoryApi()).ToArray();

			return new CollectionApi<CategoryApi>(categoriesTotalCount, skip, take, result.Length, result);
		}

		[HttpPost]
		public CategoryApi Post(int sectionId, CategoryApi categoryApi)
		{
			var section = _context.Sections
				.Where(s => s.UserId == null && s.WalletId == null && s.SharedAccountId == null)
				.FirstOrDefault(s => s.Id == sectionId);

			if (section == null)
			{
				throw new StatusCodeException(HttpStatusCode.BadRequest, new IdItem(sectionId), typeof(IdItem));
			}

			var entry = _context.Categories.Add(new Category()
			{
				Name = categoryApi.Name,
				SectionId = sectionId,
				Description = categoryApi.Description
			});

			_context.SaveChanges();

			return entry.Entity.ToCategoryApi();
		}

		[HttpPut]
		public CategoryApi Put(int sectionId, CategoryEditApi categoryApi)
		{
			var category = _context.Categories
				.Where(ExpressionIsSectionCategoryGlobal(sectionId))
				.FirstOrDefault(c => c.Id == categoryApi.Id);

			if (category == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(categoryApi.Id), typeof(IdItem));
			}

			category.Name = categoryApi.Name;
			category.Description = categoryApi.Description;
			_context.SaveChanges();

			return category.ToCategoryApi();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int sectionId, int id)
		{
			var category = _context.Categories
				.Where(ExpressionIsSectionCategoryGlobal(sectionId))
				.FirstOrDefault(c => c.Id == id);

			if (category == null)
			{
				return NotFound(new { Id = id });
			}

			_context.Categories.Remove(category);
			_context.SaveChanges();

			return NoContent();
		}
	}
}
