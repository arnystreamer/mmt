using Jimx.MMT.API.App;
using Jimx.MMT.API.Context;
using Jimx.MMT.API.Models.Common;
using Jimx.MMT.API.Models.Receipt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Jimx.MMT.API.Controllers
{
    [Route("products")]
	[ApiController]
	[Authorize]
	public class ProductsController : ControllerBase
	{
		private readonly ILogger<ProductsController> _logger;
		private readonly DbActionsWrapper<ProductApi, ProductEditApi, Product> _wrapper;

		public ProductsController(ILogger<ProductsController> logger, DbActionsWrapper<ProductApi, ProductEditApi, Product> wrapper)
		{
			_logger = logger;
			_wrapper = wrapper;
		}

		[HttpGet]
		public CollectionApi<ProductApi> GetAll([FromQuery] CollectionRequestApi requestApi)
		{
			return _wrapper.GetAll(requestApi);
		}

		[HttpGet("{id}")]
		public ProductApi Get(Guid id)
		{
			var currency = _wrapper.Get(p => p.Id  == id);

			if (currency == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new GuidItem(id), typeof(GuidItem));
			}

			return currency;
		}

		[HttpPost]
		public ProductApi Post(ProductEditApi userApi)
		{
			return _wrapper.Add(userApi);
		}

		[HttpDelete("{id}")]
		public void Delete(Guid id)
		{
			var result = _wrapper.Delete(p => p.Id == id);

			if (!result)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new GuidItem(id), typeof(GuidItem));
			}
		}
	}
}
