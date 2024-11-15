using Jimx.MMT.API.App;
using Jimx.MMT.API.Context;
using Jimx.MMT.API.Models.Common;
using Jimx.MMT.API.Models.StaticItems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Jimx.MMT.API.Controllers
{
	[ApiController]
	[Route("locations")]
	[Authorize]
	public class LocationsController : ControllerBase
	{
		private readonly ILogger<LocationsController> _logger;
		private readonly DbActionsWrapper<LocationApi, LocationEditApi, Location> _wrapper;

		public LocationsController(ILogger<LocationsController> logger, DbActionsWrapper<LocationApi, LocationEditApi, Location> wrapper)
		{
			_logger = logger;
			_wrapper = wrapper;
		}

		[HttpGet]
		public CollectionApi<LocationApi> GetAll([FromQuery] CollectionRequestApi requestApi)
		{
			return _wrapper.GetAll(requestApi);
		}

		[HttpGet("{id}")]
		public LocationApi Get(int id)
		{
			var location = _wrapper.Get(l => l.Id == id);

			if (location == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return location;
		}

		[HttpPost]
		[Authorize]
		public LocationApi Post(LocationEditApi userApi)
		{
			return _wrapper.Add(userApi);
		}

		[HttpDelete("{id}")]
		[Authorize]
		public void Delete(int id)
		{
			var result = _wrapper.Delete(l => l.Id == id);

			if (!result)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}
		}
	}
}
