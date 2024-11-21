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
	[Route("wallets/{walletId}/sections/{sectionId}/categories")]
	[Authorize]
	public class WalletsSectionsCategoriesController : ControllerBase
	{
		private readonly ILogger<WalletsSectionsCategoriesController> _logger;
		private readonly DbActionsWrapper<CategoryApi, CategoryEditApi, Category> _wrapper;
		private readonly DbActionsWrapper<WalletSectionApi, SectionEditApi, Section> _sectionsWrapper;
		private readonly DbActionsWrapper<WalletApi, WalletEditApi, Wallet> _walletWrapper;
		private readonly UserActionsWrapper _usersWrapper;

		private readonly Func<int, int, Expression<Func<Category, bool>>> ExpressionIsSectionCategoryBelongsWallet = (sectionId, walletId) =>
			c => c.Section.UserId == null && c.Section.WalletId == walletId && c.Section.SharedAccountId == null && c.Section.Id == sectionId;

		private readonly Func<int, Expression<Func<Section, bool>>> ExpressionIsSectionBelongsToWallet = (walletId) =>
			s => s.UserId == null && s.WalletId == walletId && s.SharedAccount == null;

		private readonly Func<Guid, Expression<Func<Wallet, bool>>> ExpressionIsWalletBelongsToUser = userId =>
			w => w.UserId == userId;

		public WalletsSectionsCategoriesController(ILogger<WalletsSectionsCategoriesController> logger, 
			DbActionsWrapper<CategoryApi, CategoryEditApi, Category> wrapper,
			DbActionsWrapper<WalletSectionApi, SectionEditApi, Section> sectionsWrapper,
			DbActionsWrapper<WalletApi, WalletEditApi, Wallet> walletWrapper,
			UserActionsWrapper usersWrapper)
		{
			_logger = logger;
			_wrapper = wrapper;
			_sectionsWrapper = sectionsWrapper;
			_walletWrapper = walletWrapper;
			_usersWrapper = usersWrapper;
		}

		[HttpGet("{id}")]
		public CategoryApi Get(int walletId, int sectionId, int id)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			var category = _wrapper.Get(c => c.Id == id, ExpressionIsSectionCategoryBelongsWallet(sectionId, walletId));

			if (category == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return category;
		}

		[HttpGet]
		public CollectionApi<CategoryApi> GetAll(int walletId, int sectionId, [FromQuery] CollectionRequestApi requestApi)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			return _wrapper.GetAll(requestApi, ExpressionIsSectionCategoryBelongsWallet(sectionId, walletId));
		}

		[HttpPost]
		public CategoryApi Post(int walletId, int sectionId, CategoryEditApi categoryApi)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			if (_sectionsWrapper.Get(s =>s.Id == sectionId, ExpressionIsSectionBelongsToWallet(walletId)) == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(sectionId), typeof(IdItem));
			}

			return _wrapper.Add(categoryApi, (ref Category c) => { c.SectionId = sectionId; });
		}

		[HttpPut("{id}")]
		public CategoryApi Put(int walletId, int sectionId, int id, CategoryEditApi categoryApi)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			var category = _wrapper.Edit(c => c.Id == id, categoryApi, null, ExpressionIsSectionCategoryBelongsWallet(sectionId, walletId));
			if (category == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return category;
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int walletId, int sectionId, int id)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			var result = _wrapper.Delete(c => c.Id == id, ExpressionIsSectionCategoryBelongsWallet(sectionId, walletId));
			if (!result)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return NoContent();
		}
	}
}
