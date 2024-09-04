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
	[Route("local-sections")]
	[Authorize]
	public class LocalSectionsController : ControllerBase
	{
		private readonly ApiDbContext _context;
		private readonly ILogger<LocalSectionsController> _logger;

		private readonly Func<Guid, Expression<Func<Section, bool>>> ExpressionIsSectionUserLocal = (userId) =>
			s => s.UserId == userId && s.WalletId == null && s.SharedAccountId == null;

		public LocalSectionsController(ILogger<LocalSectionsController> logger, ApiDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet("{id}")]
		public LocalSectionApi Get(int id)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var section = _context.Sections
				.Where(ExpressionIsSectionUserLocal(currentUser.Id))
				.FirstOrDefault(c => c.Id == id);

			if (section == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return section.ToLocalSectionApi();
		}

		[HttpGet]
		public CollectionApi<LocalSectionApi> GetAll([FromQuery] CollectionRequestApi requestApi)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var sectionsAll = _context.Sections
				.Where(ExpressionIsSectionUserLocal(currentUser.Id));

			var sectionsTotal = sectionsAll.Count();

			int skip = requestApi.Skip ?? 0;
			int take = requestApi.Take ?? 10;
			var result = sectionsAll
				.Skip(skip).Take(take)
				.Select(s => s.ToLocalSectionApi())
				.ToArray();

			return new CollectionApi<LocalSectionApi>(sectionsTotal, skip, take, result.Length, result);
		}

		[HttpPost]
		public LocalSectionApi Post(SectionEditApi sectionApi)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var section = new Section()
			{
				SharedAccountId = null,
				WalletId = null,
				UserId = currentUser.Id,
				Name = sectionApi.Name,
				Description = sectionApi.Description
			};

			var entity = _context.Sections.Add(section);
			_context.SaveChanges();

			return section.ToLocalSectionApi();
		}

		[HttpPut]
		public LocalSectionApi Put(SectionEditApi sectionApi)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var section = _context.Sections
				.Where(ExpressionIsSectionUserLocal(currentUser.Id))
				.FirstOrDefault(c => c.Id == sectionApi.Id);

			if (section == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(sectionApi.Id), typeof(IdItem));
			}

			section.Name = sectionApi.Name;
			section.Description = sectionApi.Description;

			_context.SaveChanges();

			return section.ToLocalSectionApi();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var section = _context.Sections
				.Where(ExpressionIsSectionUserLocal(currentUser.Id))
				.FirstOrDefault(c => c.Id == id);

			if (section == null)
			{
				return NotFound(new { Id = id, UserId = currentUser.Id });
			}

			_context.Sections.Remove(section);
			_context.SaveChanges();

			return NoContent();
		}
	}
}
