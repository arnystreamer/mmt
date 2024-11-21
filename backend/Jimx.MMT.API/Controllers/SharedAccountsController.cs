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
	[Route("shared-accounts")]
	[Authorize]
	public class SharedAccountsController : ControllerBase
	{
		private readonly ILogger<SharedAccountsController> _logger;
		private readonly DbActionsWrapper<SharedAccountApi, SharedAccountEditApi, SharedAccount> _wrapper;
		private readonly UserActionsWrapper _usersWrapper;

		private readonly Func<Guid, Expression<Func<SharedAccount, bool>>> ExpressionIsSharedAccountBelongsUser = userId =>
			sa => sa.SharedAccountToUsers.Any(sau => sau.UserId == userId);

		public SharedAccountsController(ILogger<SharedAccountsController> logger, 
			DbActionsWrapper<SharedAccountApi, SharedAccountEditApi, SharedAccount> wrapper,
			UserActionsWrapper usersWrapper)
		{
			_logger = logger;
			_wrapper = wrapper;
			_usersWrapper = usersWrapper;
		}

		[HttpGet("{id}")]
		public SharedAccountApi Get(int id)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			var sharedAccount = _wrapper.Get(c => c.Id == id, ExpressionIsSharedAccountBelongsUser(currentUser.Id));
			if (sharedAccount == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return sharedAccount;
		}

		[HttpGet]
		public CollectionApi<SharedAccountApi> GetAll([FromQuery] CollectionRequestApi requestApi)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			return _wrapper.GetAll(requestApi, ExpressionIsSharedAccountBelongsUser(currentUser.Id));
		}

		[HttpPost]
		public SharedAccountApi Post(SharedAccountEditApi sharedAccountApi)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			return _wrapper.Add(sharedAccountApi, (ref SharedAccount sa) =>
			{
				sa.SharedAccountToUsers.Add(new SharedAccountToUser()
				{
					User = currentUser
				});
			});
		}

		[HttpPut("{id}")]
		public SharedAccountApi Put(int id, SharedAccountEditApi sharedAccountApi)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			var sharedAccount = _wrapper.Edit(c => c.Id == id, sharedAccountApi, null, ExpressionIsSharedAccountBelongsUser(currentUser.Id));
			if (sharedAccount == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return sharedAccount;
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			var result = _wrapper.Delete(c => c.Id == id, ExpressionIsSharedAccountBelongsUser(currentUser.Id));

			if (!result)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return NoContent();
		}
	}
}
