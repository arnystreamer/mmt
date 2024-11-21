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
	[Route("wallets/{walletId}/sections")]
	[Authorize]
	public class WalletsSectionsController : ControllerBase
	{
		private readonly ILogger<WalletsSectionsController> _logger;
		private readonly DbActionsWrapper<WalletSectionApi, SectionEditApi, Section> _wrapper;
		private readonly DbActionsWrapper<WalletApi, WalletEditApi, Wallet> _walletWrapper;
		private readonly UserActionsWrapper _usersWrapper;

		private readonly Func<int, Guid, Expression<Func<Section, bool>>> ExpressionIsSectionWallet = (walletId, userId) =>
			s => s.UserId == null && s.WalletId == walletId && s.SharedAccountId == null && s.Wallet != null && s.Wallet.UserId == userId;

		public WalletsSectionsController(ILogger<WalletsSectionsController> logger, 
			DbActionsWrapper<WalletSectionApi, SectionEditApi, Section> wrapper,
			DbActionsWrapper<WalletApi, WalletEditApi, Wallet> walletWrapper,
			UserActionsWrapper usersWrapper)
		{
			_logger = logger;
			_wrapper = wrapper;
			_walletWrapper = walletWrapper;
			_usersWrapper = usersWrapper;
		}

		[HttpGet("{id}")]
		public WalletSectionApi Get(int walletId, int id)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			var section = _wrapper.Get(c => c.Id == id, ExpressionIsSectionWallet(walletId, currentUser.Id));
			if (section == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return section;
		}

		[HttpGet]
		public CollectionApi<WalletSectionApi> GetAll(int walletId, [FromQuery] CollectionRequestApi requestApi)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			return _wrapper.GetAll(requestApi, ExpressionIsSectionWallet(walletId, currentUser.Id));
		}

		[HttpPost]
		public WalletSectionApi Post(int walletId, [FromBody] SectionEditApi sectionApi)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			var sharedAccount = _walletWrapper.Get(sa => sa.Id == walletId, w => w.UserId == currentUser.Id);
			if (sharedAccount == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(walletId), typeof(IdItem));
			}

			return _wrapper.Add(sectionApi, (ref Section s) => { s.WalletId = walletId; });
		}

		[HttpPut("{id}")]
		public WalletSectionApi Put(int id, int walletId, [FromBody] SectionEditApi sectionApi)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			var section = _wrapper.Edit(c => c.Id == id, sectionApi, null, ExpressionIsSectionWallet(walletId, currentUser.Id));

			if (section == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return section;
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int walletId, int id)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			var result = _wrapper.Delete(c => c.Id == id, ExpressionIsSectionWallet(walletId, currentUser.Id));

			if (!result)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return NoContent();
		}
	}
}
