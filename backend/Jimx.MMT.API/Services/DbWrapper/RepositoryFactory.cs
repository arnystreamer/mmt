using Jimx.MMT.API.Context;
using Microsoft.EntityFrameworkCore;

namespace Jimx.MMT.API.Services.DbWrapper
{
	public class RepositoryFactory
	{
		private readonly ApiDbContext _context;

		public RepositoryFactory(ApiDbContext context)
		{
			_context = context;
		}

		public IRepository<TEntity> Create<TEntity>(Func<ApiDbContext, DbSet<TEntity>> entitiesSelector)
			where TEntity : class, new()
		{
			return new ProxyRepository<TEntity>(_context, entitiesSelector);
		}
	}
}
