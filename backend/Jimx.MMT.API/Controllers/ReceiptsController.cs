﻿using Jimx.MMT.API.App;
using Jimx.MMT.API.Context;
using Jimx.MMT.API.Models.Common;
using Jimx.MMT.API.Models.Receipt;
using Jimx.MMT.API.Services.Auth;
using Jimx.MMT.API.Services.DbWrapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Net;

namespace Jimx.MMT.API.Controllers
{
	[Route("receipts")]
	[ApiController]
	public class ReceiptsController : ControllerBase
	{
		private readonly ILogger<ReceiptsController> _logger;
		private readonly DbActionsWrapper<ReceiptApi, ReceiptEditApi, Receipt> _wrapper;
		private readonly UserActionsWrapper _usersWrapper;

		private readonly Func<Guid, Expression<Func<Receipt, bool>>> ExpressionIsReceiptBelongsUser = userId =>
			r => r.UserId == userId || (r.SharedAccount != null && r.SharedAccount.SharedAccountToUsers.Any(satu => satu.UserId == userId));

		public ReceiptsController(ILogger<ReceiptsController> logger, 
			DbActionsWrapper<ReceiptApi, ReceiptEditApi, Receipt> wrapper,
			UserActionsWrapper usersWrapper)
		{
			_logger = logger;
			_wrapper = wrapper;
			_usersWrapper = usersWrapper;
		}

		[HttpGet]
		public CollectionApi<ReceiptApi> GetAll([FromQuery] CollectionRequestApi requestApi)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			return _wrapper.GetAll(requestApi, ExpressionIsReceiptBelongsUser(currentUser.Id));
		}

		[HttpGet("{id}")]
		public ReceiptApi Get(Guid id)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			var receipt = _wrapper.Get(r => r.Id == id, ExpressionIsReceiptBelongsUser(currentUser.Id));
			if (receipt == null) 
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new GuidItem(id), typeof(GuidItem));
			}

			return receipt;
		}

		[HttpPost]
		public ReceiptApi Post(ReceiptEditApi receiptApi)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			return _wrapper.Add(receiptApi, (ref Receipt r) =>
			{
				r.UserId = currentUser.Id;
				r.CreateTime = DateTime.Now;
				r.CreateUserId = currentUser.Id;
			});
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(Guid id)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			var result = _wrapper.Delete(r => r.Id == id, ExpressionIsReceiptBelongsUser(currentUser.Id));
			if (!result)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new GuidItem(id), typeof(GuidItem));
			}

			return NoContent();
		}
	}
}