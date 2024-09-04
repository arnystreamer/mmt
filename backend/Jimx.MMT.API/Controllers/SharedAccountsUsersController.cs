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
	[Route("shared-accounts/{accountId}/users/")]
	[Authorize]
	public class SharedAccountsUsersController : ControllerBase
	{
		private readonly ApiDbContext _context;
		private readonly ILogger<SharedAccountsUsersController> _logger;

		private readonly Func<Guid, Expression<Func<SharedAccount, bool>>> ExpressionIsSharedAccountBelongsUser = userId =>
			sa => sa.SharedAccountToUsers.Any(sau => sau.UserId == userId);

		public SharedAccountsUsersController(ILogger<SharedAccountsUsersController> logger, ApiDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet]
		public SharedAccountUsersApi GetAll(int accountId)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var sharedAccountsWithUsers =
				_context.SharedAccounts
					.Where(sa => sa.Id == accountId)
					.Where(ExpressionIsSharedAccountBelongsUser(currentUser.Id))
					.Join(_context.SharedAccountToUsers, sa => sa.Id, satu => satu.SharedAccountId,
						(sa, satu) => new { SharedAccount = sa, SharedAccountToUser = satu })
					.Join(_context.Users, x => x.SharedAccountToUser.UserId, u => u.Id, (x, u) => new { x.SharedAccount, User = u })
					.GroupBy(y => y.SharedAccount)
					.ToList();

			if (!sharedAccountsWithUsers.Any())
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(accountId), typeof(IdItem));
			}

			var saUsers = sharedAccountsWithUsers.Single();

			return new SharedAccountUsersApi(saUsers.Key.Id, saUsers.Key.Name, saUsers.Key.Description,
				saUsers.Select(u => u.User.ToUserApi()).ToArray());
		}

		[HttpPost]
		public SharedAccountToUserApi Post(int accountId, SharedAccountToUserCreateApi userCreateApi)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var sharedAccount = _context.SharedAccounts
				.Where(ExpressionIsSharedAccountBelongsUser(currentUser.Id))
				.FirstOrDefault(sa => sa.Id == accountId);

			if (sharedAccount == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(accountId), typeof(IdItem));
			}

			if (_context.SharedAccountToUsers.Any(satu => satu.SharedAccountId == accountId && satu.UserId == currentUser.Id))
			{
				throw new StatusCodeException(HttpStatusCode.BadRequest, new IdItem(accountId), typeof(IdItem));
			}

			var entity = _context.SharedAccountToUsers.Add(new SharedAccountToUser()
			{
				SharedAccountId = accountId,
				UserId = userCreateApi.UserId,
			}).Entity;

			return new SharedAccountToUserApi(entity.Id, entity.SharedAccountId, entity.UserId);
		}

		[HttpDelete("{userId}")]
		public IActionResult Delete(int accountId, Guid userId)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var sharedAccount = _context.SharedAccounts
					.Where(ExpressionIsSharedAccountBelongsUser(currentUser.Id))
					.FirstOrDefault(sa => sa.Id == accountId);

			if (sharedAccount == null)
			{
				return NotFound();
			}

			if (currentUser.Id == userId)
			{
				return BadRequest();
			}

			var entity = _context.SharedAccountToUsers.First(satu => satu.SharedAccountId == accountId && satu.UserId == currentUser.Id);

			_context.SharedAccountToUsers.Remove(entity);
			_context.SaveChanges();

			return NoContent();
		}
	}
}
