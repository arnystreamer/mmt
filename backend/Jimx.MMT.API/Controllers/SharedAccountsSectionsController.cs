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
	[Route("shared-accounts/{accountId}/sections/")]
	[Authorize]
	public class SharedAccountsSectionsController : ControllerBase
	{
		private readonly ApiDbContext _context;
		private readonly ILogger<SharedAccountsSectionsController> _logger;

		private readonly Func<int, Expression<Func<Section, bool>>> ExpressionIsSectionSharedAccount = (sharedAccountId) =>
			s => s.UserId == null && s.WalletId == null && s.SharedAccountId == sharedAccountId;

		private readonly Func<Guid, Expression<Func<SharedAccount, bool>>> ExpressionIsSharedAccountBelongsUser = userId =>
			sa => sa.SharedAccountToUsers.Any(sau => sau.UserId == userId);

		public SharedAccountsSectionsController(ILogger<SharedAccountsSectionsController> logger, ApiDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet("{id}")]
		public SharedAccountSectionApi Get(int accountId, int id)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var section = _context.Sections
				.Where(ExpressionIsSectionSharedAccount(accountId))
				.FirstOrDefault(c => c.Id == id);

			if (section == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return section.ToSharedAccountSectionApi();
		}

		[HttpGet]
		public CollectionApi<SharedAccountSectionApi> GetAll(int accountId, [FromQuery] CollectionRequestApi requestApi)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var sharedAccount = _context.SharedAccounts
				.Where(ExpressionIsSharedAccountBelongsUser(currentUser.Id))
				.FirstOrDefault(sa => sa.Id == accountId);

			if (sharedAccount == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(accountId), typeof(IdItem));
			}

			var sectionsAll = _context.Sections.Where(ExpressionIsSectionSharedAccount(accountId));
			var sectionsCount = sectionsAll.Count();

			int skip = requestApi.Skip ?? 0;
			int take = requestApi.Take ?? 10;
			var result = sectionsAll.Skip(skip).Take(take)
				.Select(s => s.ToSharedAccountSectionApi())
				.ToArray();

			return new CollectionApi<SharedAccountSectionApi>(sectionsCount, skip, take, result.Length, result);
		}

		[HttpPost]
		public SharedAccountSectionApi Post(int accountId, SectionEditApi sectionEditApi)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var sharedAccount = _context.SharedAccounts
				.Where(ExpressionIsSharedAccountBelongsUser(currentUser.Id))
				.FirstOrDefault(sa => sa.Id == accountId);

			if (sharedAccount == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(sectionEditApi.Id), typeof(IdItem));
			}

			var entity = _context.Sections.Add(new Section()
			{
				SharedAccountId = accountId,
				UserId = null,
				WalletId = null,
				Name = sectionEditApi.Name,
				Description = sectionEditApi.Description
			}).Entity;

			_context.SaveChanges();

			return entity.ToSharedAccountSectionApi();
		}

		[HttpPut]
		public SharedAccountSectionApi Put(int accountId, SectionEditApi sectionEditApi)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var section = _context.Sections
				.Where(ExpressionIsSectionSharedAccount(accountId))
				.FirstOrDefault(c => c.Id == sectionEditApi.Id);

			if (section == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(sectionEditApi.Id), typeof(IdItem));
			}

			section.Name = sectionEditApi.Name;
			section.Description = sectionEditApi.Description;
			_context.SaveChanges();

			return section.ToSharedAccountSectionApi();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int accountId, int id)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var section = _context.Sections
				.Where(ExpressionIsSectionSharedAccount(accountId))
				.FirstOrDefault(c => c.Id == id);

			if (section == null)
			{
				return NotFound();
			}

			_context.Sections.Remove(section);
			return NoContent();
		}
	}
}
