using System.Linq.Expressions;

namespace Jimx.MMT.API.Services.DbWrapper
{
	public interface IRepositoryRequest<TEntity> : IDisposable
		where TEntity : class, new()
	{
		IQueryable<TEntity> GetAll();
		TEntity? Get(Expression<Func<TEntity, bool>> itemSelector);
		TEntity Add(TEntity entity);
		bool Delete(TEntity entity);
		void SaveChanges();
	}
}
