using Jimx.MMT.API.App;
using Jimx.MMT.API.Models.Common;
using Jimx.MMT.API.Models.StaticItems;
using Jimx.MMT.API.Services.DbWrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Jimx.MMT.API.Controllers
{
	[ApiController]
	[Route("global-users")]
	[Authorize]
	public class GlobalUsersController : ControllerBase
	{
		private readonly ILogger<GlobalUsersController> _logger;
		private readonly UserActionsWrapper _wrapper;

		public GlobalUsersController(ILogger<GlobalUsersController> logger,
			UserActionsWrapper wrapper)
		{
			_logger = logger;
			_wrapper = wrapper;
		}

		[HttpGet("{id}")]
		public UserApi Get(Guid id)
		{
			var user = _wrapper.Get(u => u.Id == id);
			if (user == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new GuidItem(id), typeof(GuidItem));
			}

			return user;
		}

		[HttpGet]
		public CollectionApi<UserApi> GetAll([FromQuery] CollectionRequestApi requestApi)
		{
			return _wrapper.GetAll(requestApi);
		}

		[HttpPost]
		public UserApi Post(UserEditApi userApi)
		{
			return _wrapper.Add(new UserEditApi(userApi.Login.ToLowerInvariant(), userApi.Name));
		}

		[HttpPut("{id}")]
		public UserApi Put(Guid id, UserEditApi userApi)
		{
			var user = _wrapper.Edit(u => u.Id == id, new UserEditApi(userApi.Login.ToLowerInvariant(), userApi.Name), null);
			if (user == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new GuidItem(id), typeof(GuidItem));
			}

			return user;
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(Guid id)
		{
			var result = _wrapper.Delete(u => u.Id == id);
			if (!result)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new GuidItem(id), typeof(GuidItem));
			}
			
			return NoContent();
		}
	}
}
