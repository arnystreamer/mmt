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
		private readonly DbActionsWrapper<GlobalSectionApi, SectionEditApi, Section> _wrapper;
		private readonly ILogger<GlobalSectionsController> _logger;

		private readonly Expression<Func<Section, bool>> ExpressionIsSectionCategoryGlobal = 
			c => c.UserId == null && c.WalletId == null && c.SharedAccountId == null;

		public GlobalSectionsController(ILogger<GlobalSectionsController> logger, 
			DbActionsWrapper<GlobalSectionApi, SectionEditApi, Section> wrapper)
		{
			_logger = logger;
			_wrapper = wrapper;
		}

		[HttpGet("{id}")]
		public GlobalSectionApi Get(int id)
		{
			var section = _wrapper.Get(s => s.Id == id, ExpressionIsSectionCategoryGlobal);
			if (section == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return section;
		}

		[HttpGet]
		public CollectionApi<GlobalSectionApi> GetAll([FromQuery] CollectionRequestApi requestApi)
		{
			return _wrapper.GetAll(requestApi, ExpressionIsSectionCategoryGlobal);
		}

		[HttpPost]
		public GlobalSectionApi Post(SectionEditApi sectionApi)
		{
			return _wrapper.Add(sectionApi, (ref Section s) => { 
				s.Wallet = null;
				s.SharedAccountId = null;
				s.UserId = null;
			});
		}

		[HttpPut("{id}")]
		public GlobalSectionApi Put(int id, SectionEditApi sectionApi)
		{
			var section = _wrapper.Edit(s => s.Id == id, sectionApi);
			if (section == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return section;
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var result = _wrapper.Delete(s => s.Id == id, ExpressionIsSectionCategoryGlobal);

			if (!result)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return NoContent();
		}
	}
}
