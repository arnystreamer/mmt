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
	[Route("local-sections")]
	[Authorize]
	public class LocalSectionsController : ControllerBase
	{
		private readonly ILogger<LocalSectionsController> _logger;
		private readonly DbActionsWrapper<LocalSectionApi, SectionEditApi, Section> _wrapper;
		private readonly UserActionsWrapper _usersWrapper;

		private readonly Func<Guid, Expression<Func<Section, bool>>> ExpressionIsSectionUserLocal = (userId) =>
			s => s.UserId == userId && s.WalletId == null && s.SharedAccountId == null;

		public LocalSectionsController(ILogger<LocalSectionsController> logger, 
			DbActionsWrapper<LocalSectionApi, SectionEditApi, Section> wrapper,
			UserActionsWrapper usersWrapper)
		{
			_logger = logger;
			_wrapper = wrapper;
			_usersWrapper = usersWrapper;
		}

		[HttpGet("{id}")]
		public LocalSectionApi Get(int id)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			var section = _wrapper.Get(c => c.Id == id, ExpressionIsSectionUserLocal(currentUser.Id));
			if (section == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return section;
		}

		[HttpGet]
		public CollectionApi<LocalSectionApi> GetAll([FromQuery] CollectionRequestApi requestApi)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			return _wrapper.GetAll(requestApi, ExpressionIsSectionUserLocal(currentUser.Id));
		}

		[HttpPost]
		public LocalSectionApi Post(SectionEditApi sectionApi)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			return _wrapper.Add(sectionApi, (ref Section s) => { s.UserId = currentUser.Id; });
			
		}

		[HttpPut("{id}")]
		public LocalSectionApi Put(int id, SectionEditApi sectionApi)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			var section = _wrapper.Edit(c => c.Id == id, sectionApi, null, ExpressionIsSectionUserLocal(currentUser.Id));

			if (section == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return section;
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			var result = _wrapper.Delete(c => c.Id == id, ExpressionIsSectionUserLocal(currentUser.Id));
			if (!result)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return NoContent();
		}
	}
}
