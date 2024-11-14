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
	[Route("sections")]
	[Authorize]
	public class SectionsController : ControllerBase
	{
		private readonly ILogger<SectionsController> _logger;
		private readonly DbActionsWrapper<SectionApi, SectionEditApi, Section> _wrapper;
		private readonly UserActionsWrapper _usersWrapper;

		private Expression<Func<Section, bool>> GetExpressionIsSectionAvailableToUser(Guid userId) => s => 
			(s.UserId == null || s.UserId == userId) &&
			(s.WalletId == null || s.Wallet!.UserId == userId) &&
			(s.SharedAccountId == null || s.SharedAccount!.SharedAccountToUsers.Any(satu => satu.UserId == userId));

		public SectionsController(ILogger<SectionsController> logger, 
			DbActionsWrapper<SectionApi, SectionEditApi, Section> wrapper,
			UserActionsWrapper usersWrapper)
		{
			_logger = logger;
			_wrapper = wrapper;
			_usersWrapper = usersWrapper;
		}

		[HttpGet("{id}")]
		public SectionApi Get(int id)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			var section = _wrapper.Get(s => s.Id == id, GetExpressionIsSectionAvailableToUser(currentUser.Id));
			if (section == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return section;
		}

		[HttpGet]
		public CollectionApi<SectionApi> GetAll([FromQuery] CollectionRequestApi requestApi)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			return _wrapper.GetAll(requestApi, GetExpressionIsSectionAvailableToUser(currentUser.Id));
		}
	}
}
