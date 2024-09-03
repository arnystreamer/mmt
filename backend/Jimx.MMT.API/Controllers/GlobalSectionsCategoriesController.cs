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
	[Route("global-sections/{sectionId}/categories/")]
	[Authorize]
	public class GlobalSectionsCategoriesController : ControllerBase
	{
		private readonly ApiDbContext _context;
		private readonly ILogger<GlobalSectionsCategoriesController> _logger;

		public GlobalSectionsCategoriesController(ILogger<GlobalSectionsCategoriesController> logger, ApiDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet("{id}")]
		public CategoryApi Get(int sectionId, int id)
		{
			var category = _context.Categories
				.Where(c => c.Section.UserId == null && c.Section.WalletId == null && c.Section.SharedAccountId == null)
				.FirstOrDefault(c => c.SectionId == sectionId && c.Id == id);

			if (category == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return new CategoryApi(category.Id, category.SectionId, category.Name, category.Description);
		}

		[HttpGet]
		public CollectionApi<CategoryApi> GetAll(int sectionId, [FromQuery] CollectionRequestApi requestApi)
		{
			var count = _context.Categories
				.Where(c => c.SectionId == sectionId)
				.Where(c => c.Section.UserId == null && c.Section.WalletId == null && c.Section.SharedAccountId == null)
				.Count();

			int skip = requestApi.Skip ?? 0;
			int take = requestApi.Take ?? 10;
			var categories = _context.Categories
				.Where(c => c.SectionId == sectionId)
				.Where(c => c.Section.UserId == null && c.Section.WalletId == null && c.Section.SharedAccountId == null)
				.Skip(skip).Take(take).ToList();

			IList<CategoryApi> result = new List<CategoryApi>();
			foreach (var category in categories)
			{
				result.Add(new CategoryApi(category.Id, category.SectionId, category.Name, category.Description));
			}

			return new CollectionApi<CategoryApi>(count, skip, take, result.Count, result.ToArray());
		}

		[HttpPost]
		public CategoryApi Post(int sectionId, CategoryApi categoryApi)
		{
			var section = _context.Sections
				.Where(s => s.UserId == null && s.WalletId == null && s.SharedAccountId == null)
				.FirstOrDefault(s => s.Id == sectionId);

			if (section == null)
			{
				throw new StatusCodeException(HttpStatusCode.BadRequest, new IdItem(sectionId), typeof(IdItem));
			}

			var entry = _context.Categories.Add(new Category()
			{
				Name = categoryApi.Name,
				SectionId = sectionId,
				Description = categoryApi.Description
			});

			_context.SaveChanges();

			return new CategoryApi(entry.Entity.Id, entry.Entity.SectionId, entry.Entity.Name, entry.Entity.Description);
		}

		[HttpPut]
		public CategoryApi Put(int sectionId, CategoryApi categoryApi)
		{
			var category = _context.Categories
				.Where(c => c.SectionId == sectionId)
				.FirstOrDefault(c => c.Id == categoryApi.Id);
			if (category == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(categoryApi.Id), typeof(IdItem));
			}

			var section = _context.Sections
				.Where(s => s.UserId == null && s.WalletId == null && s.SharedAccountId == null)
				.FirstOrDefault(s => s.Id == sectionId);

			if (section == null)
			{
				throw new StatusCodeException(HttpStatusCode.BadRequest, new IdItem(categoryApi.SectionId), typeof(IdItem));
			}

			category.Name = categoryApi.Name;
			category.SectionId = categoryApi.SectionId;
			category.Description = categoryApi.Description;
			_context.SaveChanges();

			return categoryApi;
		}

		[HttpDelete("{id}")]
		public void Delete(int sectionId, int id)
		{
			var category = _context.Categories
				.Where(c => c.SectionId == sectionId)
				.FirstOrDefault(c => c.Id == id);
			if (category == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			_context.Categories.Remove(category);
			_context.SaveChanges();

			return;
		}
	}
}
