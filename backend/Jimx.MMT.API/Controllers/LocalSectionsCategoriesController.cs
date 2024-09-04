using Jimx.MMT.API.App;
using Jimx.MMT.API.Context;
using Jimx.MMT.API.Models.Common;
using Jimx.MMT.API.Models.StaticItems;
using Jimx.MMT.API.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Net;

namespace Jimx.MMT.API.Controllers
{
	[ApiController]
	[Route("local-sections/{sectionId}/categories/")]
	[Authorize]
	public class LocalSectionsCategoriesController : ControllerBase
	{
		private readonly ApiDbContext _context;
		private readonly ILogger<LocalSectionsCategoriesController> _logger;

		private readonly Func<int, Guid, Expression<Func<Category, bool>>> ExpressionIsSectionCategoryUserLocal = (sectionId, userId) =>
			c => c.Section.UserId == userId && c.Section.WalletId == null && c.Section.SharedAccountId == null && c.Section.Id == sectionId;

		private readonly Func<Guid, Expression<Func<Section, bool>>> ExpressionIsSectionBelongsToUser = userId =>
			s => s.UserId == userId && s.WalletId == null && s.SharedAccount == null;

		public LocalSectionsCategoriesController(ILogger<LocalSectionsCategoriesController> logger, ApiDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet("{id}")]
		public CategoryApi Get(int sectionId, int id)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var category = _context.Categories
				.Where(ExpressionIsSectionCategoryUserLocal(sectionId, currentUser.Id))
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
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			if (!_context.Sections.Where(ExpressionIsSectionBelongsToUser(currentUser.Id))
				.Any(c => c.Id == sectionId))
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(sectionId), typeof(IdItem));
			}

			var categoriesCount = _context.Categories
				.Where(ExpressionIsSectionCategoryUserLocal(sectionId, currentUser.Id))
				.Count();

			int skip = requestApi.Skip ?? 0;
			int take = requestApi.Take ?? 10;
			var result = _context.Categories
				.Where(ExpressionIsSectionCategoryUserLocal(sectionId, currentUser.Id))
				.Skip(skip).Take(take)
				.Select(c => c.ToCategoryApi())
				.ToArray();

			return new CollectionApi<CategoryApi>(categoriesCount, skip, take, result.Length, result);
		}

		[HttpPost]
		public CategoryApi Post(int sectionId, CategoryEditApi categoryApi)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			if (!_context.Sections.Where(ExpressionIsSectionBelongsToUser(currentUser.Id))
				.Any(c => c.Id == sectionId))
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(sectionId), typeof(IdItem));
			}

			var category = new Category()
			{
				SectionId = sectionId,
				Name = categoryApi.Name,
				Description = categoryApi.Description
			};

			_context.Categories.Add(category);
			_context.SaveChanges();

			return category.ToCategoryApi();
		}

		[HttpPut]
		public CategoryApi Put(int sectionId, CategoryEditApi categoryApi)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var category = _context.Categories
				.Where(ExpressionIsSectionCategoryUserLocal(sectionId, currentUser.Id))
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
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var category = _context.Categories
				.Where(ExpressionIsSectionCategoryUserLocal(sectionId, currentUser.Id))
				.FirstOrDefault(c => c.Id == id);

			if (category == null)
			{
				return NotFound(new { Id = id, SectionId = sectionId, UserId = currentUser.Id });
			}

			_context.Categories.Remove(category);

			return NoContent();
		}
	}
}
