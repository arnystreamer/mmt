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
	[Route("shared-accounts/{accountId}/sections/{sectionId}/categories/")]
	[Authorize]
	public class SharedAccountsSectionsCategoriesController : ControllerBase
	{
		private readonly ILogger<SharedAccountsSectionsCategoriesController> _logger;
		private readonly DbActionsWrapper<CategoryApi, CategoryEditApi, Category> _wrapper;
		private readonly DbActionsWrapper<SharedAccountSectionApi, SectionEditApi, Section> _sectionsWrapper;
		private readonly DbActionsWrapper<SharedAccountApi, SharedAccountEditApi, SharedAccount> _sharedAccountWrapper;
		private readonly UserActionsWrapper _usersWrapper;

		private readonly Func<int, int, Expression<Func<Category, bool>>> ExpressionIsSectionCategoryBelongsToSharedAccount = (sectionId, sharedAccountId) =>
			c => c.Section.UserId == null && c.Section.WalletId == null && c.Section.SharedAccountId == sharedAccountId && c.Section.Id == sectionId;

		private readonly Func<int, int, Expression<Func<Section, bool>>> ExpressionIsSectionBelongsToSharedAccount = (id, sharedAccountId) =>
			s => s.UserId == null && s.WalletId == null && s.SharedAccountId == sharedAccountId && s.Id == id;

		public SharedAccountsSectionsCategoriesController(ILogger<SharedAccountsSectionsCategoriesController> logger, 
			DbActionsWrapper<CategoryApi, CategoryEditApi, Category> wrapper,
			DbActionsWrapper<SharedAccountSectionApi, SectionEditApi, Section> sectionsWrapper,
			DbActionsWrapper<SharedAccountApi, SharedAccountEditApi, SharedAccount> sharedAccountWrapper,
			UserActionsWrapper usersWrapper)
		{
			_logger = logger;
			_wrapper = wrapper;
			_sectionsWrapper = sectionsWrapper;
			_sharedAccountWrapper = sharedAccountWrapper;
			_usersWrapper = usersWrapper;
		}

		[HttpGet("{id}")]
		public CategoryApi Get(int accountId, int sectionId, int id)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			var category = _wrapper.Get(c => c.Id == id, ExpressionIsSectionCategoryBelongsToSharedAccount(sectionId, accountId));

			if (category == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return category;
		}

		[HttpGet]
		public CollectionApi<CategoryApi> GetAll(int accountId, int sectionId, [FromQuery] CollectionRequestApi requestApi)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			return _wrapper.GetAll(requestApi, ExpressionIsSectionCategoryBelongsToSharedAccount(sectionId, accountId));
		}

		[HttpPost]
		public CategoryApi Post(int accountId, int sectionId, CategoryEditApi categoryApi)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			if (_sectionsWrapper.Count(ExpressionIsSectionBelongsToSharedAccount(sectionId, accountId)) == 0)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(sectionId), typeof(IdItem));
			}

			return _wrapper.Add(categoryApi, (ref Category c) => { c.SectionId = sectionId; });
		}

		[HttpPut("{id}")]
		public CategoryApi Put(int accountId, int sectionId, int id, CategoryEditApi categoryApi)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			var category = _wrapper.Edit(c => c.Id == id, categoryApi, null, ExpressionIsSectionCategoryBelongsToSharedAccount(sectionId, accountId));
			if (category == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return category;
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int accountId, int sectionId, int id)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			var result = _wrapper.Delete(c => c.Id == id, ExpressionIsSectionCategoryBelongsToSharedAccount(sectionId, accountId));
			if (!result)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return NoContent();
		}
	}
}
