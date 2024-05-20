using Microsoft.EntityFrameworkCore;

namespace Jimx.MMT.API.Context
{
	public class ApiDbContext : DbContext
	{
		public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
		{

		}
	}
}
