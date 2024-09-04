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
	[Route("shared-accounts/{accountId}/sections/{sectionId}/categories/")]
	[Authorize]
	public class SharedAccountsSectionsCategoriesController : ControllerBase
	{
		private readonly ApiDbContext _context;
		private readonly ILogger<SharedAccountsSectionsCategoriesController> _logger;

		private readonly Func<int, int, Expression<Func<Category, bool>>> ExpressionIsSectionCategoryBelongsToSharedAccount = (sectionId, sharedAccountId) =>
			c => c.Section.UserId == null && c.Section.WalletId == null && c.Section.SharedAccountId == sharedAccountId && c.Section.Id == sectionId;

		private readonly Func<int, Expression<Func<Section, bool>>> ExpressionIsSectionBelongsToSharedAccount = sharedAccountId =>
			s => s.UserId == null && s.WalletId == null && s.SharedAccountId == sharedAccountId;

		public SharedAccountsSectionsCategoriesController(ILogger<SharedAccountsSectionsCategoriesController> logger, ApiDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet("{id}")]
		public CategoryApi Get(int accountId, int sectionId, int id)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var category = _context.Categories
				.Where(ExpressionIsSectionCategoryBelongsToSharedAccount(sectionId, accountId))
				.FirstOrDefault(c => c.Id == id);

			if (category == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return category.ToCategoryApi();
		}

		[HttpGet]
		public CollectionApi<CategoryApi> GetAll(int accountId, int sectionId, [FromQuery] CollectionRequestApi requestApi)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			if (!_context.Sections.Where(ExpressionIsSectionBelongsToSharedAccount(accountId))
				.Any(c => c.Id == sectionId))
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(sectionId), typeof(IdItem));
			}

			var categoriesCount = _context.Categories
				.Where(ExpressionIsSectionCategoryBelongsToSharedAccount(sectionId, accountId))
				.Count();

			int skip = requestApi.Skip ?? 0;
			int take = requestApi.Take ?? 10;
			var categories = _context.Categories
				.Where(ExpressionIsSectionCategoryBelongsToSharedAccount(sectionId, accountId))
				.Skip(skip).Take(take).ToList();

			var result = categories.Select(c => c.ToCategoryApi()).ToArray();

			return new CollectionApi<CategoryApi>(categoriesCount, skip, take, result.Length, result.ToArray());
		}

		[HttpPost]
		public CategoryApi Post(int accountId, int sectionId, CategoryEditApi categoryApi)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			if (!_context.Sections.Where(ExpressionIsSectionBelongsToSharedAccount(accountId))
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
		public CategoryApi Put(int accountId, int sectionId, CategoryEditApi categoryApi)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var category = _context.Categories
				.Where(ExpressionIsSectionCategoryBelongsToSharedAccount(sectionId, accountId))
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
		public IActionResult Delete(int accountId, int sectionId, int id)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var category = _context.Categories
				.Where(ExpressionIsSectionCategoryBelongsToSharedAccount(sectionId, accountId))
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
