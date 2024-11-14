using Jimx.MMT.API.App;
using Jimx.MMT.API.Context;
using Jimx.MMT.API.Models.Common;
using Jimx.MMT.API.Models.StaticItems;
using Jimx.MMT.API.Services.Auth;
using Jimx.MMT.API.Services.DbWrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Net;

namespace Jimx.MMT.API.Controllers
{
	[ApiController]
	[Route("sections/{sectionId}/categories/")]
	[Authorize]
	public class SectionsCategoriesController : ControllerBase
	{
		private readonly ILogger<SectionsCategoriesController> _logger;
		private readonly DbActionsWrapper<CategoryApi, CategoryEditApi, Category> _wrapper;
		private readonly UserActionsWrapper _usersWrapper;

		private Expression<Func<Category, bool>> GetExpressionIsSectionCategoryBelongsToUser(Guid userId) => c =>
			(c.Section.UserId == null || c.Section.UserId == userId) &&
			(c.Section.WalletId == null || c.Section.Wallet!.UserId == userId) &&
			(c.Section.SharedAccountId == null || c.Section.SharedAccount!.SharedAccountToUsers.Any(satu => satu.UserId == userId));

		public SectionsCategoriesController(ILogger<SectionsCategoriesController> logger, 
			DbActionsWrapper<CategoryApi, CategoryEditApi, Category> wrapper,
			UserActionsWrapper usersWrapper)
		{
			_logger = logger;
			_wrapper = wrapper;
			_usersWrapper = usersWrapper;
		}

		[HttpGet("{id}")]
		public CategoryApi Get(int sectionId, int id)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			var category = _wrapper.Get(c => c.Id == id && c.SectionId == sectionId, GetExpressionIsSectionCategoryBelongsToUser(currentUser.Id));

			if (category == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return category;
		}

		[HttpGet]
		public CollectionApi<CategoryApi> GetAll(int sectionId, [FromQuery] CollectionRequestApi requestApi)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			return _wrapper.GetAll(requestApi, GetExpressionIsSectionCategoryBelongsToUser(currentUser.Id));
		}
	}
}
