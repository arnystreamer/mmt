using Microsoft.EntityFrameworkCore;

namespace Jimx.MMT.API.Services.DbWrapper
{
	public interface ICustomDbSetConvertationProvider<TEntity, TApi> : IGetModelMapper<TEntity, TApi>
		where TEntity : class
	{
		IQueryable<TEntity> DbSetToQueryable(DbSet<TEntity> dbSet); 
	}
}
