using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Jimx.MMT.API.Services.DbWrapper
{
	public class RepositoryRequest<TEntity> : IRepositoryRequest<TEntity>
		where TEntity : class, new()
	{
		private readonly IEnumerable<Expression<Func<TEntity, bool>>> _whereSelectors;
		private readonly DbSet<TEntity> _entities;
		private readonly IRepository<TEntity> _repository;
		private readonly bool _noSaveRequest;

		internal RepositoryRequest(
			Expression<Func<TEntity, bool>>?[] selectors, 
			DbSet<TEntity> entities,
			IRepository<TEntity> repository,
			bool noSaveRequest) 
		{
			_whereSelectors = selectors.Where(s => s != null).Select(s => s!);
			_entities = entities;
			_repository = repository;
			_noSaveRequest = noSaveRequest;
		}

		public TEntity Add(TEntity entity)
		{
			if (_noSaveRequest)
			{
				throw new InvalidOperationException("Cannot invoke Add when request is create as no-save");
			}

			var entry = _entities.Add(entity);
			_repository.SaveChanges();

			return entry.Entity;
		}

		public bool Delete(TEntity entity)
		{
			if (_noSaveRequest)
			{
				throw new InvalidOperationException("Cannot invoke Delete when request is create as no-save");
			}

			_entities.Remove(entity);
			_repository.SaveChanges();
			return true;
		}

		public void Dispose()
		{
			if (!_noSaveRequest)
			{
				SaveChanges();
			}
		}

		public TEntity? Get(Expression<Func<TEntity, bool>> itemSelector)
		{
			return GetAll().SingleOrDefault(itemSelector);
		}

		public IQueryable<TEntity> GetAll()
		{
			IQueryable<TEntity> entities = _entities;

			foreach (var whereSelector in _whereSelectors)
			{
				entities = entities.Where(whereSelector);
			}

			return entities;
		}

		public void SaveChanges()
		{
			if (_noSaveRequest)
			{
				throw new InvalidOperationException("Cannot invoke SaveChanges when request is create as no-save");
			}

			_repository.SaveChanges();
		}
	}
}
