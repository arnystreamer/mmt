using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Jimx.MMT.API.Services.DbWrapper
{
	public interface IRepository<TEntity>
		where TEntity : class, new()
	{
		IRepositoryRequest<TEntity> StartRequest(bool noSaveRequest, params Expression<Func<TEntity, bool>>?[] selectors);
		void SaveChanges();
	}
}
