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
	[Route("global-users")]
	[Authorize]
	public class GlobalUsersController : ControllerBase
	{
		private readonly ApiDbContext _context;
		private readonly ILogger<GlobalUsersController> _logger;

		public GlobalUsersController(ILogger<GlobalUsersController> logger, ApiDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet("{id}")]
		public UserApi Get(Guid id)
		{
			var user = _context.Users.FirstOrDefault(u => u.Id == id);

			if (user == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new GuidItem(id), typeof(GuidItem));
			}

			return user.ToUserApi();
		}

		[HttpGet]
		public CollectionApi<UserApi> GetAll([FromQuery] CollectionRequestApi requestApi)
		{
			var userAll = _context.Users;

			var usersTotalCount = userAll.Count();

			int skip = requestApi.Skip ?? 0;
			int take = requestApi.Take ?? 10;
			var result = userAll
				.Skip(skip).Take(take)
				.Select(s => s.ToUserApi())
				.ToArray();

			return new CollectionApi<UserApi>(usersTotalCount, skip, take, result.Length, result);
		}

		[HttpPost]
		public UserApi Post(UserApi userApi)
		{
			var user = new User()
			{
				Login = userApi.Login.ToLower(),
				Name = userApi.Name
			};

			var entity = _context.Users.Add(user);
			_context.SaveChanges();

			return entity.Entity.ToUserApi();
		}

		[HttpPut]
		public UserApi Put(UserApi userApi)
		{
			var user = _context.Users.FirstOrDefault(u => u.Id == userApi.Id);

			if (user == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new GuidItem(userApi.Id), typeof(GuidItem));
			}

			user.Login = userApi.Login.ToLower();
			user.Name = userApi.Name;

			_context.SaveChanges();

			return user.ToUserApi();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(Guid id)
		{
			var user = _context.Users.FirstOrDefault(u => u.Id == id);

			if (user == null)
			{
				return NotFound(new { Id = id });
			}

			_context.SaveChanges();
			return NoContent();
		}
	}
}
