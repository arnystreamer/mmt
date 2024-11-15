using Jimx.MMT.API.App;
using Jimx.MMT.API.Context;
using Jimx.MMT.API.Models.Common;
using Jimx.MMT.API.Models.Receipt;
using Jimx.MMT.API.Services.Auth;
using Jimx.MMT.API.Services.DbWrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Net;

namespace Jimx.MMT.API.Controllers
{
    [Route("receipts/{receiptId}/entries")]
	[ApiController]
	[Authorize]
	public class ReceiptEntriesController : ControllerBase
	{
		private readonly ILogger<ReceiptEntriesController> _logger;
		private readonly DbActionsWrapper<ReceiptEntryApi, ReceiptEntryEditApi, ReceiptEntry> _wrapper;
		private readonly DbActionsWrapper<ReceiptApi, ReceiptEditApi, Receipt> _receiptWrapper;
		private readonly DbActionsWrapper<ProductApi, ProductEditApi, Product> _productWrapper;
		private readonly UserActionsWrapper _usersWrapper;

		private readonly Func<Guid, Expression<Func<ReceiptEntry, bool>>> ExpressionIsEntriesReceiptBelongsUser = 
			userId =>
				re => 
				(re.Receipt.UserId == userId 
					|| (re.Receipt.SharedAccount != null && re.Receipt.SharedAccount.SharedAccountToUsers.Any(satu => satu.UserId == userId))) 
				&& 
				((re.Product.Section.UserId == null && re.Product.Section.WalletId == null && re.Product.Section.SharedAccountId == null)
					|| re.Product.Section.UserId == userId 
					|| (re.Product.Section.Wallet != null && re.Product.Section.Wallet.UserId == userId)
					|| (re.Product.Section.SharedAccount != null && re.Product.Section.SharedAccount.SharedAccountToUsers.Any(satu => satu.UserId == userId))
					&& (re.Product.UserId == null || re.Product.UserId == userId)
					&& !re.Product.IsDeleted
				);

		private readonly Func<Guid, Expression<Func<Product, bool>>> ExpressionIsProductBelongsUser =
			userId =>
			p => (p.Section.UserId == null && p.Section.WalletId == null && p.Section.SharedAccountId == null)
					|| p.Section.UserId == userId
					|| (p.Section.Wallet != null && p.Section.Wallet.UserId == userId)
					|| (p.Section.SharedAccount != null && p.Section.SharedAccount.SharedAccountToUsers.Any(satu => satu.UserId == userId))
					&& (p.UserId == null || p.UserId == userId)
					&& !p.IsDeleted;

		private readonly Func<Guid, Expression<Func<Receipt, bool>>> ExpressionIsReceiptBelongsUser =
			userId =>
				r => (r.UserId == userId || (r.SharedAccount != null && r.SharedAccount.SharedAccountToUsers.Any(satu => satu.UserId == userId)));

		public ReceiptEntriesController(ILogger<ReceiptEntriesController> logger, 
			DbActionsWrapper<ReceiptEntryApi, ReceiptEntryEditApi, ReceiptEntry> wrapper,
			DbActionsWrapper<ReceiptApi, ReceiptEditApi, Receipt> receiptWrapper,
			DbActionsWrapper<ProductApi, ProductEditApi, Product> productWrapper,
			UserActionsWrapper usersWrapper)
		{
			_logger = logger;
			_wrapper = wrapper;
			_receiptWrapper = receiptWrapper;
			_productWrapper = productWrapper;
			_usersWrapper = usersWrapper;
		}

		[HttpGet]
		public CollectionApi<ReceiptEntryApi> GetAll(Guid receiptId, [FromQuery] CollectionRequestApi requestApi)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			return _wrapper.GetAll(requestApi, ExpressionIsEntriesReceiptBelongsUser(currentUser.Id));
		}

		[HttpGet("{id}")]
		public ReceiptEntryApi Get(Guid receiptId, Guid id)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			var currency = _wrapper.Get(re => re.ReceiptId == receiptId && re.Id == id, ExpressionIsEntriesReceiptBelongsUser(currentUser.Id));

			if (currency == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new GuidItem(id), typeof(GuidItem));
			}

			return currency;
		}

		[HttpPost]
		public ReceiptEntryApi Post(Guid receiptId, ReceiptEntryEditApi userApi)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			if (_receiptWrapper.Get(r => r.Id == receiptId, ExpressionIsReceiptBelongsUser(currentUser.Id)) == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new GuidItem(receiptId), typeof(GuidItem));
			}

			if (_productWrapper.Get(p => p.Id == userApi.ProductId && !p.IsDeleted, ExpressionIsProductBelongsUser(currentUser.Id)) == null)
			{
				throw new StatusCodeException(HttpStatusCode.BadRequest, new GuidItem(userApi.ProductId), typeof(GuidItem));
			}

			return _wrapper.Add(userApi, (ref ReceiptEntry re) =>
			{
				re.CreateTime = DateTime.Now;
				re.CreateUserId = currentUser.Id;
			});
		}

		[HttpDelete("{id}")]
		public void Delete(Guid receiptId, Guid id)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			var result = _wrapper.Delete(re => re.ReceiptId == receiptId && re.Id == id, ExpressionIsEntriesReceiptBelongsUser(currentUser.Id));

			if (!result)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new GuidItem(id), typeof(GuidItem));
			}
		}
	}
}
