using Jimx.MMT.API.App;
using Jimx.MMT.API.Context;
using Jimx.MMT.API.Models.Common;
using Jimx.MMT.API.Models.StaticItems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Jimx.MMT.API.Controllers
{
	[ApiController]
	[Route("wallet/{walletId}/section/")]
	[Authorize]
	public class SectionForWalletController : ControllerBase
	{
		private readonly ApiDbContext _context;
		private readonly ILogger<SectionForWalletController> _logger;

		public SectionForWalletController(ILogger<SectionForWalletController> logger, ApiDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet("{id}")]
		public SectionApi Get(int walletId, int id)
		{
			var currentUserId = Guid.Empty;

			var section = _context.Sections.Include(s => s.Wallet).Where(s => s.WalletId == walletId).FirstOrDefault(c => c.Id == id);
			if (section == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			if (section.Wallet.UserId != currentUserId)
			{
				throw new StatusCodeException(HttpStatusCode.Forbidden);
			}

			return new SectionApi(section.Id, section.WalletId.Value, section.Name, section.Description);
		}

		[HttpGet]
		public CollectionApi<SectionApi> GetAll(int walletId, [FromQuery] CollectionRequestApi requestApi)
		{
			var count = _context.Sections.Where(s => s.WalletId == walletId).Count();

			int skip = requestApi.Skip ?? 0;
			int take = requestApi.Take ?? 10;
			var sections = _context.Sections.Skip(skip).Take(take).ToList();

			IList<SectionApi> result = new List<SectionApi>();
			foreach (var section in sections)
			{
				result.Add(new SectionApi(section.Id, section.WalletId.Value, section.Name, section.Description));
			}

			return new CollectionApi<SectionApi>(count, skip, take, result.Count, result.ToArray());
		}

		[HttpPost]
		public SectionApi Post(int walletId, SectionForWalletEditApi sectionApi)
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
			return new SectionApi(entity.Id, entity.WalletId.Value, entity.Name, entity.Description);
		}

		[HttpPut]
		public SectionApi Put(int walletId, SectionForWalletEditApi sectionApi)
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

			return new SectionApi(section.Id, section.WalletId.Value, section.Name, section.Description);
		}

		[HttpDelete("{id}")]
		public void Delete(int walletId, int id)
		{
			var currentUserId = Guid.Empty;

			var section = _context.Sections.Include(s => s.Wallet).FirstOrDefault(c => c.WalletId == walletId && c.Id == id);
			if (section == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			if (section.Wallet.UserId != currentUserId)
			{
				throw new StatusCodeException(HttpStatusCode.Forbidden);
			}

			_context.Sections.Remove(section);
			_context.SaveChanges();

			return;
		}

		[HttpGet("{id}/categories")]
		public SectionCategoriesApi GetCategories(int walletId, int id)
		{
			var currentUserId = Guid.Empty;

			var section = _context.Sections.Include(s => s.Categories).FirstOrDefault(c => c.WalletId == walletId && c.Id == id);
			if (section == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			if (section.Wallet.UserId != currentUserId)
			{
				throw new StatusCodeException(HttpStatusCode.Forbidden);
			}

			List<CategoryApi> categoryApis = new List<CategoryApi>();
			foreach (var category in section.Categories)
			{
				categoryApis.Add(new CategoryApi(category.Id, category.SectionId, category.Name, category.Description));
			}

			return new SectionCategoriesApi(section.Id, section.WalletId.Value, section.Name, section.Description, categoryApis.ToArray());
		}
	}
}
