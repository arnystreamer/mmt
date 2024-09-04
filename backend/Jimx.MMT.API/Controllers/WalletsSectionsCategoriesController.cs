using Jimx.MMT.API.App;
using Jimx.MMT.API.Context;
using Jimx.MMT.API.Models.Common;
using Jimx.MMT.API.Models.StaticItems;
using Jimx.MMT.API.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;

namespace Jimx.MMT.API.Controllers
{
	[ApiController]
	[Route("wallets/{walletId}/sections/{sectionId}/categories/")]
	[Authorize]
	public class WalletsSectionsCategoriesController : ControllerBase
	{
		private readonly ApiDbContext _context;
		private readonly ILogger<WalletsSectionsCategoriesController> _logger;

		private readonly Func<int, int, Expression<Func<Category, bool>>> ExpressionIsSectionCategoryBelongsWallet = (sectionId, walletId) =>
			c => c.Section.UserId == null && c.Section.WalletId == walletId && c.Section.SharedAccountId == null && c.Section.Id == sectionId;

		private readonly Func<int, Expression<Func<Section, bool>>> ExpressionIsSectionBelongsToWallet = walletId =>
			s => s.UserId == null && s.WalletId == walletId && s.SharedAccount == null;

		public WalletsSectionsCategoriesController(ILogger<WalletsSectionsCategoriesController> logger, ApiDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet("{id}")]
		public CategoryApi Get(int walletId, int sectionId, int id)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var category = _context.Categories
				.Where(ExpressionIsSectionCategoryBelongsWallet(sectionId, walletId))
				.FirstOrDefault(c => c.Id == id);

			if (category == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return category.ToCategoryApi();
		}

		[HttpGet]
		public CollectionApi<CategoryApi> GetAll(int walletId, int sectionId, [FromQuery] CollectionRequestApi requestApi)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var section = _context.Sections.Include(s => s.Categories).FirstOrDefault(c => c.WalletId == walletId && c.Id == sectionId);
			if (section == null || section.Wallet == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(sectionId), typeof(IdItem));
			}

			if (section.Wallet.UserId != currentUser.Id)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(sectionId), typeof(IdItem));
			}

			List<CategoryApi> categoryApis = new List<CategoryApi>();
			foreach (var category in section.Categories)
			{
				categoryApis.Add(new CategoryApi(category.Id, category.SectionId, category.Name, category.Description));
			}

			throw new NotImplementedException();
		}

		[HttpPost]
		public CategoryApi Post(int walletId, int sectionId, CategoryEditApi categoryApi)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			if (!_context.Sections.Where(ExpressionIsSectionBelongsToWallet(walletId))
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
		public CategoryApi Put(int walletId, int sectionId, CategoryEditApi categoryApi)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var category = _context.Categories
				.Where(ExpressionIsSectionCategoryBelongsWallet(sectionId, walletId))
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
		public IActionResult Delete(int walletId, int sectionId, int id)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var category = _context.Categories
				.Where(ExpressionIsSectionCategoryBelongsWallet(sectionId, walletId))
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
