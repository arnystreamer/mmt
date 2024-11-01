using Jimx.MMT.API.App;
using Jimx.MMT.API.Context;
using Jimx.MMT.API.Models.Common;
using Jimx.MMT.API.Models.StaticItems;
using Jimx.MMT.API.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;

namespace Jimx.MMT.API.Controllers
{
	[ApiController]
	[Route("wallets/{walletId}/sections")]
	[Authorize]
	public class WalletsSectionsController : ControllerBase
	{
		private readonly ApiDbContext _context;
		private readonly ILogger<WalletsSectionsController> _logger;

		private readonly Func<int, Expression<Func<Section, bool>>> ExpressionIsSectionWallet = (walletId) =>
			s => s.UserId == null && s.WalletId == walletId && s.SharedAccountId == null;

		public WalletsSectionsController(ILogger<WalletsSectionsController> logger, ApiDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet("{id}")]
		public WalletSectionApi Get(int walletId, int id)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var section = _context.Sections.Include(s => s.Wallet)
				.Where(ExpressionIsSectionWallet(walletId)).FirstOrDefault(c => c.Id == id);

			if (section == null || section.Wallet == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			if (section.Wallet.UserId != currentUser.Id)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return new WalletSectionApi(section.Id, section.WalletId.Value, section.Name, section.Description);
		}

		[HttpGet]
		public CollectionApi<WalletSectionApi> GetAll(int walletId, [FromQuery] CollectionRequestApi requestApi)
		{
			var sectionsAll = _context.Sections.Where(ExpressionIsSectionWallet(walletId));

			int count = sectionsAll.Count();
			int skip = requestApi.Skip ?? 0;
			int take = requestApi.Take ?? 10;
			var result = _context.Sections.Where(s => s.WalletId == walletId)
				.Skip(skip).Take(take)
				.Select(s => s.ToWalletSectionApi())
				.ToList();

			return new CollectionApi<WalletSectionApi>(count, skip, take, result.Count, result.ToArray());
		}

		[HttpPost]
		public WalletSectionApi Post(int walletId, [FromBody] SectionEditApi sectionApi)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var newWallet = _context.Wallets.FirstOrDefault(w => w.Id == walletId);
			if (newWallet == null)
			{
				throw new StatusCodeException(HttpStatusCode.BadRequest);
			}

			if (newWallet.UserId != currentUser.Id)
			{
				throw new StatusCodeException(HttpStatusCode.BadRequest);
			}

			var entry = _context.Sections.Add(new Section()
			{
				WalletId = walletId,
				UserId = null,
				Name = sectionApi.Name,
				Description = sectionApi.Description
			});

			_context.SaveChanges();

			Section entity = entry.Entity;
			return new WalletSectionApi(entity.Id, entity.WalletId!.Value, entity.Name, entity.Description);
		}

		[HttpPut]
		public WalletSectionApi Put(int walletId, [FromBody] SectionEditApi sectionApi)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var section = _context.Sections.FirstOrDefault(c => c.Id == sectionApi.Id);
			if (section == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(sectionApi.Id), typeof(IdItem));
			}

			var newWallet = _context.Wallets.FirstOrDefault(w => w.Id == walletId);
			if (newWallet == null)
			{
				throw new StatusCodeException(HttpStatusCode.BadRequest);
			}

			if (newWallet.UserId != currentUser.Id) 
			{
				throw new StatusCodeException(HttpStatusCode.BadRequest);
			}

			section.WalletId = walletId;
			section.UserId = null;
			section.Name = sectionApi.Name;
			section.Description = sectionApi.Description;

			_context.SaveChanges();

			return new WalletSectionApi(section.Id, section.WalletId.Value, section.Name, section.Description);
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int walletId, int id)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var section = _context.Sections.Include(s => s.Wallet).FirstOrDefault(c => c.WalletId == walletId && c.Id == id);
			if (section == null || section.Wallet == null)
			{
				return NotFound();
			}

			if (section.Wallet.UserId != currentUser.Id)
			{
				return NotFound();
			}

			_context.Sections.Remove(section);
			_context.SaveChanges();

			return NoContent();
		}
	}
}
