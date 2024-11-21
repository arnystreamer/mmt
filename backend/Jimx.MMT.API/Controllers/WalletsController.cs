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
	[Route("wallets")]
	[Authorize]
	public class WalletsController : ControllerBase
	{
		private readonly ILogger<WalletsController> _logger;
		private readonly DbActionsWrapper<WalletApi, WalletEditApi, Wallet> _wrapper;
		private readonly UserActionsWrapper _usersWrapper;

		private readonly Func<Guid, Expression<Func<Wallet, bool>>> ExpressionIsWalletBelongsUser = userId =>
			w => w.UserId == userId;

		public WalletsController(ILogger<WalletsController> logger, 
			DbActionsWrapper<WalletApi, WalletEditApi, Wallet> wrapper,
			UserActionsWrapper usersWrapper)
		{
			_logger = logger;
			_wrapper = wrapper;
			_usersWrapper = usersWrapper;
		}

		[HttpGet("{id}")]
		public WalletApi Get(int id)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			var wallet = _wrapper.Get(c => c.Id == id, ExpressionIsWalletBelongsUser(currentUser.Id));
			if (wallet == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return new WalletApi(wallet.Id, currentUser.Id, wallet.Name, wallet.Description);
		}

		[HttpGet]
		public CollectionApi<WalletApi> GetAll([FromQuery] CollectionRequestApi requestApi)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			return _wrapper.GetAll(requestApi, ExpressionIsWalletBelongsUser(currentUser.Id));
		}

		[HttpPost]
		public WalletApi Post([FromBody] WalletEditApi walletApi)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			return _wrapper.Add(walletApi, (ref Wallet w) => { w.UserId = currentUser.Id; });
		}

		[HttpPut("{id}")]
		public WalletApi Put(int id, [FromBody] WalletEditApi walletApi)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			var wallet = _wrapper.Edit(c => c.Id == id, walletApi, null, ExpressionIsWalletBelongsUser(currentUser.Id));
			if (wallet == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return wallet;
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			var result = _wrapper.Delete(c => c.Id == id, ExpressionIsWalletBelongsUser(currentUser.Id));
			if (!result)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return NoContent();
		}
	}
}
