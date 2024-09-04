using Jimx.MMT.API.App;
using Jimx.MMT.API.Context;
using Jimx.MMT.API.Models.Common;
using Jimx.MMT.API.Models.StaticItems;
using Jimx.MMT.API.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Net;

namespace Jimx.MMT.API.Controllers
{
	[ApiController]
	[Route("shared-accounts")]
	[Authorize]
	public class SharedAccountsController : ControllerBase
	{
		private readonly ApiDbContext _context;
		private readonly ILogger<SharedAccountsController> _logger;

		private readonly Func<Guid, Expression<Func<SharedAccount, bool>>> ExpressionIsSharedAccountBelongsUser = userId =>
			sa => sa.SharedAccountToUsers.Any(sau => sau.UserId == userId);

		public SharedAccountsController(ILogger<SharedAccountsController> logger, ApiDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet("{id}")]
		public SharedAccountApi Get(int id)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var sharedAccount = _context.SharedAccounts
				.Where(ExpressionIsSharedAccountBelongsUser(currentUser.Id))
				.FirstOrDefault(c => c.Id == id);

			if (sharedAccount == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return sharedAccount.ToSharedAccountApi();
		}

		[HttpGet]
		public CollectionApi<SharedAccountApi> GetAll([FromQuery] CollectionRequestApi requestApi)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var count = _context.SharedAccounts.Where(sa => sa.SharedAccountToUsers.Any(sau => sau.UserId == currentUser.Id)).Count();

			int skip = requestApi.Skip ?? 0;
			int take = requestApi.Take ?? 10;
			var sharedAccounts = _context.SharedAccounts.Where(sa => sa.SharedAccountToUsers.Any(sau => sau.UserId == currentUser.Id))
				.Skip(skip).Take(take).ToList();

			IList<SharedAccountApi> result = new List<SharedAccountApi>();
			foreach (var sharedAccount in sharedAccounts)
			{
				result.Add(sharedAccount.ToSharedAccountApi());
			}

			return new CollectionApi<SharedAccountApi>(count, skip, take, result.Count, result.ToArray());
		}

		[HttpPost]
		public SharedAccountApi Post(SharedAccountApi sharedAccountApi)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var sharedAccount = new SharedAccount()
			{
				Name = sharedAccountApi.Name,
				Description = sharedAccountApi.Description
			};

			_context.SharedAccounts.Add(sharedAccount);

			var sharedAccountToUser = new SharedAccountToUser()
			{
				SharedAccount = sharedAccount,
				User = currentUser
			};

			sharedAccount.SharedAccountToUsers.Add(sharedAccountToUser);
			_context.SaveChanges();

			return sharedAccount.ToSharedAccountApi();
		}

		[HttpPut]
		public SharedAccountApi Put(SharedAccountApi sharedAccountApi)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var sharedAccount = _context.SharedAccounts
				.Where(ExpressionIsSharedAccountBelongsUser(currentUser.Id))
				.FirstOrDefault(c => c.Id == sharedAccountApi.Id);

			if (sharedAccount == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(sharedAccountApi.Id), typeof(IdItem));
			}

			sharedAccount.Name = sharedAccountApi.Name;
			sharedAccount.Description = sharedAccountApi.Description;
			_context.SaveChanges();

			return sharedAccount.ToSharedAccountApi();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var sharedAccount = _context.SharedAccounts
				.Where(ExpressionIsSharedAccountBelongsUser(currentUser.Id))
				.FirstOrDefault(c => c.Id == id);

			if (sharedAccount == null)
			{
				return NotFound();
			}

			_context.SharedAccounts.Remove(sharedAccount);
			_context.SaveChanges();

			return Ok();
		}
	}
}
