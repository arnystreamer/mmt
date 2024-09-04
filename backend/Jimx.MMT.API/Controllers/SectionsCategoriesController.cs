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
	[Route("sections/{sectionId}/categories/")]
	[Authorize]
	public class SectionsCategoriesController : ControllerBase
	{
		private readonly ApiDbContext _context;
		private readonly ILogger<SectionsCategoriesController> _logger;

		private Expression<Func<Section, bool>> GetExpressionIsSectionAvailableToUser(Guid userId) => s =>
			(s.UserId == null || s.UserId == userId) &&
			(s.WalletId == null || s.Wallet!.UserId == userId) &&
			(s.SharedAccountId == null || s.SharedAccount!.SharedAccountToUsers.Any(satu => satu.UserId == userId));

		public SectionsCategoriesController(ILogger<SectionsCategoriesController> logger, ApiDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet("{id}")]
		public CategoryApi Get(int sectionId, int id)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var category = _context.Categories.FirstOrDefault(c => c.Id == id);

			if (category == null || category.SectionId != sectionId)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			var section = _context.Sections
				.Where(GetExpressionIsSectionAvailableToUser(currentUser.Id))
				.FirstOrDefault(s => s.Id == sectionId);

			if (section == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(sectionId), typeof(IdItem));
			}

			return category.ToCategoryApi();
		}

		[HttpGet]
		public CollectionApi<CategoryApi> GetAll(int sectionId, [FromQuery] CollectionRequestApi requestApi)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var section = _context.Sections
				.Include(s => s.Categories)
				.Where(GetExpressionIsSectionAvailableToUser(currentUser.Id))
				.FirstOrDefault(s => s.Id == sectionId);

			if (section == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(sectionId), typeof(IdItem));
			}

			var categoriesTotalCount = section.Categories.Count();

			int skip = requestApi.Skip ?? 0;
			int take = requestApi.Take ?? 10;
			var result = section.Categories
				.Skip(skip).Take(take)
				.Select(s => s.ToCategoryApi())
				.ToArray();

			return new CollectionApi<CategoryApi>(categoriesTotalCount, skip, take, result.Length, result);
		}
	}
	}
