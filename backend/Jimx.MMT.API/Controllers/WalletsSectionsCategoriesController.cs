using Jimx.MMT.API.App;
using Jimx.MMT.API.Context;
using Jimx.MMT.API.Models.Common;
using Jimx.MMT.API.Models.StaticItems;
using Jimx.MMT.API.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Jimx.MMT.API.Controllers
{
	[ApiController]
	[Route("wallets/{walletId}/sections/{sectionId}/categories/")]
	[Authorize]
	public class WalletsSectionsCategoriesController : ControllerBase
	{
		private readonly ApiDbContext _context;
		private readonly ILogger<WalletsSectionsCategoriesController> _logger;

		public WalletsSectionsCategoriesController(ILogger<WalletsSectionsCategoriesController> logger, ApiDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet("{id}")]
		public CategoryApi Get(int walletId, int sectionId, int id)
		{
			throw new NotImplementedException();
		}

		[HttpGet]
		public CollectionApi<CategoryApi> GetAll(int walletId, int sectionId, [FromQuery] CollectionRequestApi requestApi)
		{
			var currentUser = _context.Users.GetCurrentUserFromContext(User);

			var section = _context.Sections.Include(s => s.Categories).FirstOrDefault(c => c.WalletId == walletId && c.Id == sectionId);
			if (section == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(sectionId), typeof(IdItem));
			}

			if (section.Wallet.UserId != currentUser.Id)
			{
				throw new StatusCodeException(HttpStatusCode.Forbidden);
			}

			List<CategoryApi> categoryApis = new List<CategoryApi>();
			foreach (var category in section.Categories)
			{
				categoryApis.Add(new CategoryApi(category.Id, category.SectionId, category.Name, category.Description));
			}

			throw new NotImplementedException();
		}

		[HttpPost]
		public CategoryApi Post(int walletId, int sectionId, SectionForWalletEditApi sectionApi)
		{
			throw new NotImplementedException();
		}

		[HttpPut]
		public CategoryApi Put(int walletId, int sectionId, SectionForWalletEditApi sectionApi)
		{
			throw new NotImplementedException();
		}

		[HttpDelete("{id}")]
		public void Delete(int walletId, int sectionId, int id)
		{
			throw new NotImplementedException();
		}
	}
}
