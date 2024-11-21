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
	[Route("local-sections/{sectionId}/categories/")]
	[Authorize]
	public class LocalSectionsCategoriesController : ControllerBase
	{
		private readonly ILogger<LocalSectionsCategoriesController> _logger;
		private readonly DbActionsWrapper<CategoryApi, CategoryEditApi, Category> _wrapper;
		private readonly DbActionsWrapper<LocalSectionApi, SectionEditApi, Section> _sectionsWrapper;
		private readonly UserActionsWrapper _usersWrapper;

		private readonly Func<int, Guid, Expression<Func<Category, bool>>> ExpressionIsSectionCategoryUserLocal = (sectionId, userId) =>
			c => c.Section.UserId == userId && c.Section.WalletId == null && c.Section.SharedAccountId == null && c.Section.Id == sectionId;

		private readonly Func<int, Guid, Expression<Func<Section, bool>>> ExpressionIsSectionBelongsToUser = (id, userId) =>
			s => s.UserId == userId && s.WalletId == null && s.SharedAccount == null && s.Id == id;

		public LocalSectionsCategoriesController(ILogger<LocalSectionsCategoriesController> logger, 
			DbActionsWrapper<CategoryApi, CategoryEditApi, Category> wrapper,
			DbActionsWrapper<LocalSectionApi, SectionEditApi, Section> sectionsWrapper,
			UserActionsWrapper usersWrapper)
		{
			_logger = logger;
			_wrapper = wrapper;
			_sectionsWrapper = sectionsWrapper;
			_usersWrapper = usersWrapper;
		}

		[HttpGet("{id}")]
		public CategoryApi Get(int sectionId, int id)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			var category = _wrapper.Get(c => c.Id == id, ExpressionIsSectionCategoryUserLocal(sectionId, currentUser.Id));

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

			return _wrapper.GetAll(requestApi, ExpressionIsSectionCategoryUserLocal(sectionId, currentUser.Id));
		}

		[HttpPost]
		public CategoryApi Post(int sectionId, CategoryEditApi categoryApi)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			if (_sectionsWrapper.Count(ExpressionIsSectionBelongsToUser(sectionId, currentUser.Id)) == 0)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(sectionId), typeof(IdItem));
			}

			return _wrapper.Add(categoryApi, (ref Category c) => { c.SectionId = sectionId; });
		}

		[HttpPut("{id}")]
		public CategoryApi Put(int sectionId, int id, CategoryEditApi categoryApi)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			if (_sectionsWrapper.Count(ExpressionIsSectionBelongsToUser(sectionId, currentUser.Id)) == 0)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(sectionId), typeof(IdItem));
			}

			var category = _wrapper.Edit(c => c.Id == id, categoryApi, null, ExpressionIsSectionCategoryUserLocal(sectionId, currentUser.Id));
			if (category == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return category;
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int sectionId, int id)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			var result = _wrapper.Delete(c => c.Id == id, ExpressionIsSectionCategoryUserLocal(sectionId, currentUser.Id));
			if (!result)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return NoContent();
		}
	}
}
