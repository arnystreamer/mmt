using Jimx.MMT.API.App;
using Jimx.MMT.API.Context;
using Jimx.MMT.API.Models.Common;
using Jimx.MMT.API.Models.StaticItems;
using Microsoft.AspNetCore.Mvc;
namespace Jimx.MMT.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CategoryController : ControllerBase
	{
		private readonly ApiDbContext _context;
		private readonly ILogger<CategoryController> _logger;

		public CategoryController(ILogger<CategoryController> logger, ApiDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet("{id}")]
		public CategoryApi Get(int id)
		{
			try
			{
				var category = _context.Categories.FirstOrDefault(c => c.Id == id);
				if (category == null)
				{
					throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
				}

				return new CategoryApi(category.Id, category.Name, category.Description);
			}
			catch (StatusCodeException)
			{
				throw;
			}
			catch (Exception ex)
			{
				_logger.LogWarning(ex, $"GET Category /{id}");
				throw;
			}
		}

		[HttpPost]
		public CategoryApi Post(CategoryApi categoryApi)
		{
			try 
			{ 
				var entry = _context.Categories.Add(new Category()
				{
					Name = categoryApi.Name,
					Description = categoryApi.Description
				});

				_context.SaveChanges();

				return new CategoryApi(entry.Entity.Id, entry.Entity.Name, entry.Entity.Description);
			}
			catch (StatusCodeException)
			{
				throw;
			}
			catch (Exception ex)
			{
				_logger.LogWarning(ex, $"POST Category: Name={categoryApi.Name}, Description={categoryApi.Description}");
				throw;
			}
		}

		[HttpPut]
		public CategoryApi Put(CategoryApi categoryApi)
		{
			try
			{ 
				var category = _context.Categories.FirstOrDefault(c => c.Id == categoryApi.Id);
				if (category == null)
				{
					throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, new IdItem(categoryApi.Id), typeof(IdItem));
				}

				category.Name = categoryApi.Name;
				category.Description = categoryApi.Description;
				_context.SaveChanges();

				return categoryApi;
			}
			catch (StatusCodeException)
			{
				throw;
			}
			catch (Exception ex)
			{
				_logger.LogWarning(ex, $"PUT Category: ID={categoryApi.Id}, Name={categoryApi.Name}, Description={categoryApi.Description}");
				throw;
			}
		}

		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			try
			{ 
				var category = _context.Categories.FirstOrDefault(c => c.Id == id);
				if (category == null)
				{
					throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
				}

				return;
			}
			catch (StatusCodeException)
			{
				throw;
			}
			catch (Exception ex)
			{
				_logger.LogWarning(ex, $"DELETE Category /{id}");
				throw;
			}
		}
	}
}
