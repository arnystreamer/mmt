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
	[Route("shared-accounts/{accountId}/sections/")]
	[Authorize]
	public class SharedAccountsSectionsController : ControllerBase
	{
		private readonly ILogger<SharedAccountsSectionsController> _logger;
		private readonly DbActionsWrapper<SharedAccountSectionApi, SectionEditApi, Section> _wrapper;
		private readonly DbActionsWrapper<SharedAccountApi, SharedAccountEditApi, SharedAccount> _sharedAccountWrapper;
		private readonly UserActionsWrapper _usersWrapper;

		private readonly Func<int, Guid, Expression<Func<Section, bool>>> ExpressionIsSectionSharedAccount = (sharedAccountId, userId) =>
			s => s.UserId == null && s.WalletId == null && s.SharedAccountId == sharedAccountId 
				&& (s.SharedAccount != null && s.SharedAccount.SharedAccountToUsers.Any(sau => sau.UserId == userId));

		private readonly Func<Guid, Expression<Func<SharedAccount, bool>>> ExpressionIsSharedAccountBelongsUser = userId =>
			sa => sa.SharedAccountToUsers.Any(sau => sau.UserId == userId);

		public SharedAccountsSectionsController(ILogger<SharedAccountsSectionsController> logger, 
			DbActionsWrapper<SharedAccountSectionApi, SectionEditApi, Section> wrapper,
			DbActionsWrapper<SharedAccountApi, SharedAccountEditApi, SharedAccount> sharedAccountWrapper,
			UserActionsWrapper usersWrapper)
		{
			_logger = logger;
			_wrapper = wrapper;
			_sharedAccountWrapper = sharedAccountWrapper;
			_usersWrapper = usersWrapper;
		}

		[HttpGet("{id}")]
		public SharedAccountSectionApi Get(int accountId, int id)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			var section = _wrapper.Get(c => c.Id == id, ExpressionIsSectionSharedAccount(accountId, currentUser.Id));
			if (section == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return section;
		}

		[HttpGet]
		public CollectionApi<SharedAccountSectionApi> GetAll(int accountId, [FromQuery] CollectionRequestApi requestApi)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			return _wrapper.GetAll(requestApi, ExpressionIsSectionSharedAccount(accountId, currentUser.Id));
		}

		[HttpPost]
		public SharedAccountSectionApi Post(int accountId, SectionEditApi sectionEditApi)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			var sharedAccount = _sharedAccountWrapper.Get(sa => sa.Id == accountId, ExpressionIsSharedAccountBelongsUser(currentUser.Id));
			if (sharedAccount == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(accountId), typeof(IdItem));
			}

			return _wrapper.Add(sectionEditApi, (ref Section s) => { s.SharedAccountId = accountId; });
		}

		[HttpPut("{id}")]
		public SharedAccountSectionApi Put(int id, int accountId, SectionEditApi sectionEditApi)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			var section = _wrapper.Edit(c => c.Id == id, sectionEditApi, ExpressionIsSectionSharedAccount(accountId, currentUser.Id));

			if (section == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return section;
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int accountId, int id)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			var result = _wrapper.Delete(c => c.Id == id, ExpressionIsSectionSharedAccount(accountId, currentUser.Id));

			if (!result)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return NoContent();
		}
	}
}
