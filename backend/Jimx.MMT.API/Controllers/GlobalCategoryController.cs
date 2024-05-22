using Jimx.MMT.API.App;
using Jimx.MMT.API.Context;
using Jimx.MMT.API.Models.Common;
using Jimx.MMT.API.Models.StaticItems;
using Microsoft.AspNetCore.Mvc;
using System.Net;
namespace Jimx.MMT.API.Controllers
{
	[ApiController]
	[Route("global-category")]
	public class GlobalCategoryController : ControllerBase
	{
		private readonly ApiDbContext _context;
		private readonly ILogger<GlobalCategoryController> _logger;

		public GlobalCategoryController(ILogger<GlobalCategoryController> logger, ApiDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet("{id}")]
		public CategoryApi Get(int id)
		{
			var category = _context.Categories.FirstOrDefault(c => c.Id == id);
			if (category == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return new CategoryApi(category.Id, category.SectionId, category.Name, category.Description);
		}

		[HttpGet]
		public CollectionApi<CategoryApi> GetAll([FromQuery] CollectionRequestApi requestApi)
		{
			var count = _context.Categories.Count();

			int skip = requestApi.Skip ?? 0;
			int take = requestApi.Take ?? 10;
			var categories = _context.Categories.Skip(skip).Take(take).ToList();

			IList<CategoryApi> result = new List<CategoryApi>();
			foreach (var category in categories)
			{
				result.Add(new CategoryApi(category.Id, category.SectionId, category.Name, category.Description));
			}

			return new CollectionApi<CategoryApi>(count, skip, take, result.Count, result.ToArray());
		}

		[HttpPost]
		public CategoryApi Post(CategoryApi categoryApi)
		{
			var entry = _context.Categories.Add(new Category()
			{
				Name = categoryApi.Name,
				SectionId = categoryApi.SectionId,
				Description = categoryApi.Description
			});

			_context.SaveChanges();

			return new CategoryApi(entry.Entity.Id, entry.Entity.SectionId, entry.Entity.Name, entry.Entity.Description);
		}

		[HttpPut]
		public CategoryApi Put(CategoryApi categoryApi)
		{
			var category = _context.Categories.FirstOrDefault(c => c.Id == categoryApi.Id);
			if (category == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(categoryApi.Id), typeof(IdItem));
			}

			category.Name = categoryApi.Name;
			category.SectionId = categoryApi.SectionId;
			category.Description = categoryApi.Description;
			_context.SaveChanges();

			return categoryApi;
		}

		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			var category = _context.Categories.FirstOrDefault(c => c.Id == id);
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
