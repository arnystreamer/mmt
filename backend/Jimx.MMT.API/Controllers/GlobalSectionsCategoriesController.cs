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
		private readonly ILogger<GlobalSectionsCategoriesController> _logger;
		private readonly DbActionsWrapper<CategoryApi, CategoryEditApi, Category> _wrapper;
		private readonly DbActionsWrapper<GlobalSectionApi, SectionEditApi, Section> _sectionsWrapper;

		private readonly Func<int, Expression<Func<Category, bool>>> ExpressionIsSectionCategoryGlobal = sectionId =>
			c => c.Section.UserId == null && c.Section.WalletId == null && c.Section.SharedAccountId == null && c.Section.Id == sectionId;

		private readonly Func<int, Expression<Func<Section, bool>>> ExpressionIsSectionGlobal = id =>
			s => s.UserId == null && s.WalletId == null && s.SharedAccountId == null && s.Id == id;

		public GlobalSectionsCategoriesController(ILogger<GlobalSectionsCategoriesController> logger, 
			DbActionsWrapper<CategoryApi, CategoryEditApi, Category> wrapper,
			DbActionsWrapper<GlobalSectionApi, SectionEditApi, Section> sectionsWrapper)
		{
			_logger = logger;
			_wrapper = wrapper;
			_sectionsWrapper = sectionsWrapper;
		}

		[HttpGet("{id}")]
		public CategoryApi Get(int sectionId, int id)
		{
			var category = _wrapper.Get(c => c.Id == id, ExpressionIsSectionCategoryGlobal(sectionId));

			if (category == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return category;
		}

		[HttpGet]
		public CollectionApi<CategoryApi> GetAll(int sectionId, [FromQuery] CollectionRequestApi requestApi)
		{
			return _wrapper.GetAll(requestApi, ExpressionIsSectionCategoryGlobal(sectionId));
		}

		[HttpPost]
		public CategoryApi Post(int sectionId, CategoryEditApi categoryApi)
		{
			if (_sectionsWrapper.Count(ExpressionIsSectionGlobal(sectionId)) == 0)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(sectionId), typeof(IdItem));
			}

			return _wrapper.Add(categoryApi, (ref Category c) => { c.SectionId = sectionId; });
		}

		[HttpPut("{id}")]
		public CategoryApi Put(int sectionId, int id, CategoryEditApi categoryApi)
		{
			if (_sectionsWrapper.Count(ExpressionIsSectionGlobal(sectionId)) == 0)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(sectionId), typeof(IdItem));
			}

			var category = _wrapper.Edit(c => c.Id == id, categoryApi, ExpressionIsSectionCategoryGlobal(sectionId));
			if (category == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return category;
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int sectionId, int id)
		{
			var result = _wrapper.Delete(c => c.Id == id, ExpressionIsSectionCategoryGlobal(sectionId));
			if (!result)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return NoContent();
		}
	}
}
