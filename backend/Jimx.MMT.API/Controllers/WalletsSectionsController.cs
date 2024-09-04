using Jimx.MMT.API.App;
using Jimx.MMT.API.Context;
using Jimx.MMT.API.Models.Common;
using Jimx.MMT.API.Models.StaticItems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;

namespace Jimx.MMT.API.Controllers
{
	[ApiController]
	[Route("wallets/{walletId}/sections/")]
	[Authorize]
	public class WalletsSectionsController : ControllerBase
	{
		private readonly ApiDbContext _context;
		private readonly ILogger<WalletsSectionsController> _logger;

		private readonly Func<Guid, Expression<Func<Section, bool>>> ExpressionIsSectionUserLocal = (userId) =>
			s => s.UserId == userId && s.WalletId == null && s.SharedAccountId == null;

		public WalletsSectionsController(ILogger<WalletsSectionsController> logger, ApiDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet("{id}")]
		public WalletSectionApi Get(int walletId, int id)
		{
			var currentUserId = Guid.Empty;

			var section = _context.Sections.Include(s => s.Wallet).Where(s => s.WalletId == walletId).FirstOrDefault(c => c.Id == id);
			if (section == null || section.Wallet == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			if (section.Wallet.UserId != currentUserId)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return new WalletSectionApi(section.Id, section.WalletId.Value, section.Name, section.Description);
		}

		[HttpGet]
		public CollectionApi<WalletSectionApi> GetAll(int walletId, [FromQuery] CollectionRequestApi requestApi)
		{
			var count = _context.Sections.Where(s => s.WalletId == walletId).Count();

			int skip = requestApi.Skip ?? 0;
			int take = requestApi.Take ?? 10;
			var sections = _context.Sections.Skip(skip).Take(take).ToList();

			IList<WalletSectionApi> result = new List<WalletSectionApi>();
			foreach (var section in sections)
			{
				result.Add(new WalletSectionApi(section.Id, section.WalletId!.Value, section.Name, section.Description));
			}

			return new CollectionApi<WalletSectionApi>(count, skip, take, result.Count, result.ToArray());
		}

		[HttpPost]
		public WalletSectionApi Post(int walletId, SectionEditApi sectionApi)
		{
			var currentUserId = Guid.Empty;

			var newWallet = _context.Wallets.FirstOrDefault(w => w.Id == walletId);
			if (newWallet == null)
			{
				throw new StatusCodeException(HttpStatusCode.BadRequest);
			}

			if (newWallet.UserId != currentUserId)
			{
				throw new StatusCodeException(HttpStatusCode.BadRequest);
			}

			var entry = _context.Sections.Add(new Section()
			{
				WalletId = walletId,
				UserId = currentUserId,
				Name = sectionApi.Name,
				Description = sectionApi.Description
			});

			_context.SaveChanges();

			Section entity = entry.Entity;
			return new WalletSectionApi(entity.Id, entity.WalletId!.Value, entity.Name, entity.Description);
		}

		[HttpPut]
		public WalletSectionApi Put(int walletId, SectionEditApi sectionApi)
		{
			var currentUserId = Guid.Empty;

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

			if (newWallet.UserId != currentUserId) 
			{
				throw new StatusCodeException(HttpStatusCode.BadRequest);
			}

			section.WalletId = walletId;
			section.UserId = currentUserId;
			section.Name = sectionApi.Name;
			section.Description = sectionApi.Description;

			_context.SaveChanges();

			return new WalletSectionApi(section.Id, section.WalletId.Value, section.Name, section.Description);
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int walletId, int id)
		{
			var currentUserId = Guid.Empty;

			var section = _context.Sections.Include(s => s.Wallet).FirstOrDefault(c => c.WalletId == walletId && c.Id == id);
			if (section == null || section.Wallet == null)
			{
				return NotFound();
			}

			if (section.Wallet.UserId != currentUserId)
			{
				return NotFound();
			}

			_context.Sections.Remove(section);
			_context.SaveChanges();

			return NoContent();
		}
	}
}
