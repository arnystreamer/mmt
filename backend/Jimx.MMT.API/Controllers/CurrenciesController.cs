using Jimx.MMT.API.App;
using Jimx.MMT.API.Context;
using Jimx.MMT.API.Models.Common;
using Jimx.MMT.API.Models.StaticItems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Jimx.MMT.API.Controllers
{
	[Route("currencies")]
	[ApiController]
	public class CurrenciesController : ControllerBase
	{
		private readonly ILogger<CurrenciesController> _logger;
		private readonly DbActionsWrapper<CurrencyApi, CurrencyEditApi, Currency> _wrapper;

		public CurrenciesController(ILogger<CurrenciesController> logger, DbActionsWrapper<CurrencyApi, CurrencyEditApi, Currency> wrapper)
		{
			_logger = logger;
			_wrapper = wrapper;
		}

		[HttpGet]
		public CollectionApi<CurrencyApi> GetAll([FromQuery] CollectionRequestApi requestApi)
		{
			return _wrapper.GetAll(requestApi);
		}

		[HttpGet("{id}")]
		public CurrencyApi Get(int id)
		{
			var currency = _wrapper.Get(c => c.Id == id);

			if (currency == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return currency;
		}

		[HttpPost]
		[Authorize]
		public CurrencyApi Post(CurrencyEditApi userApi)
		{
			return _wrapper.Add(userApi);
		}

		[HttpDelete("{id}")]
		[Authorize]
		public void Delete(int id)
		{
			var result = _wrapper.Delete(c => c.Id == id);

			if (!result)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}
		}
	}
}
