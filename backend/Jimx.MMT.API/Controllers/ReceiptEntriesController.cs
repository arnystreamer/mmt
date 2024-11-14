using Jimx.MMT.API.App;
using Jimx.MMT.API.Context;
using Jimx.MMT.API.Models.Common;
using Jimx.MMT.API.Models.Receipt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

		public ReceiptEntriesController(ILogger<ReceiptEntriesController> logger, 
			DbActionsWrapper<ReceiptEntryApi, ReceiptEntryEditApi, ReceiptEntry> wrapper)
		{
			_logger = logger;
			_wrapper = wrapper;
		}

		[HttpGet]
		public CollectionApi<ReceiptEntryApi> GetAll(Guid receiptId, [FromQuery] CollectionRequestApi requestApi)
		{
			return _wrapper.GetAll(requestApi);
		}

		[HttpGet("{id}")]
		public ReceiptEntryApi Get(Guid receiptId, Guid id)
		{
			var currency = _wrapper.Get(re => re.ReceiptId == receiptId && re.Id == id);

			if (currency == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new GuidItem(id), typeof(GuidItem));
			}

			return currency;
		}

		[HttpPost]
		public ReceiptEntryApi Post(Guid receiptId, ReceiptEntryEditApi userApi)
		{
			return _wrapper.Add(userApi);
		}

		[HttpDelete("{id}")]
		public void Delete(Guid receiptId, Guid id)
		{
			var result = _wrapper.Delete(re => re.ReceiptId == receiptId && re.Id == id);

			if (!result)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new GuidItem(id), typeof(GuidItem));
			}
		}
	}
}
