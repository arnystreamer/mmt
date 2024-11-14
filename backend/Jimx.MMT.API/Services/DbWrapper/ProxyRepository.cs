using Jimx.MMT.API.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Jimx.MMT.API.Services.DbWrapper
{
	public class ProxyRepository<TEntity> : IRepository<TEntity>
		where TEntity : class, new()
	{
		private readonly ApiDbContext _context;
		private readonly Func<ApiDbContext, DbSet<TEntity>> _entities;
		private Expression<Func<TEntity, bool>> _whereSelector;

		public ProxyRepository(ApiDbContext context, Func<ApiDbContext, DbSet<TEntity>> entities)
		{
			_context = context;
			_entities = entities;
			_whereSelector = x => true;
		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}

		public IRepositoryRequest<TEntity> StartRequest(bool noSaveRequest, params Expression<Func<TEntity, bool>>?[] selectors)
		{
			return new RepositoryRequest<TEntity>(selectors, _entities(_context), this, noSaveRequest);
		}
	}
}
