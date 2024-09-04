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
	[Route("sections")]
	[Authorize]
	public class SectionsController : ControllerBase
	{
		private readonly ApiDbContext _context;
		private readonly ILogger<SectionsController> _logger;

		private Expression<Func<Section, bool>> GetExpressionIsSectionAvailableToUser(Guid userId) => s => 
			(s.UserId == null || s.UserId == userId) &&
			(s.WalletId == null || s.Wallet!.UserId == userId) &&
			(s.SharedAccountId == null || s.SharedAccount!.SharedAccountToUsers.Any(satu => satu.UserId == userId));

		public SectionsController(ILogger<SectionsController> logger, ApiDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet("{id}")]
		public SectionApi Get(int id)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var section = _context.Sections
				.Where(GetExpressionIsSectionAvailableToUser(currentUser.Id))
				.FirstOrDefault(s => s.Id == id);

			if (section == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return section.ToSectionApi();
		}

		[HttpGet]
		public CollectionApi<SectionApi> GetAll([FromQuery] CollectionRequestApi requestApi)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var sectionsAll = _context.Sections
				.Where(GetExpressionIsSectionAvailableToUser(currentUser.Id));

			var sectionTotalCount = sectionsAll.Count();

			int skip = requestApi.Skip ?? 0;
			int take = requestApi.Take ?? 10;
			var result = sectionsAll
				.Skip(skip).Take(take)
				.Select(s => s.ToSectionApi())
				.ToArray();

			return new CollectionApi<SectionApi>(sectionTotalCount, skip, take, result.Length, result);
		}
	}
}
