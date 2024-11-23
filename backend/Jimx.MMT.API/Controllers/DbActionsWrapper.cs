using Jimx.MMT.API.Models.Common;
using Jimx.MMT.API.Services.DbWrapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Jimx.MMT.API.Controllers
{
	public class DbActionsWrapper<TApi, TEditApi, TEntity>
		where TApi : class
		where TEditApi : class
		where TEntity : class, new()
	{
		protected readonly IRepository<TEntity> Repository;
		protected readonly IGetModelMapper<TEntity, TApi> GetModelMapper;
		protected readonly IEditEntityMapper<TEntity, TEditApi> EditEntityMapper;
		protected List<Expression<Func<TEntity, bool>>> GlobalSelectors { get; private set; } = new List<Expression<Func<TEntity, bool>>>();

		public DbActionsWrapper(IRepository<TEntity> repository, 
			IGetModelMapper<TEntity, TApi> getModelMapper,
			IEditEntityMapper<TEntity, TEditApi> editEntityMapper)
		{
			Repository = repository;
			GetModelMapper = getModelMapper;
			EditEntityMapper = editEntityMapper;

			if (getModelMapper is ICustomDbSetConvertationProvider<TEntity, TApi> customDbSetConvertationProvider)
			{
				Repository.SetCustomConverterFunction(customDbSetConvertationProvider.DbSetToQueryable);
			}
		}

		public void AddGlobalCondition(Expression<Func<TEntity, bool>> selector)
		{
			GlobalSelectors.Add(selector);
		}

		public virtual int Count(params Expression<Func<TEntity, bool>>[] selectors)
		{
			var allSelectors = selectors.Union(GlobalSelectors).ToArray();
			using (var request = Repository.StartRequest(true, allSelectors))
			{
				return request.GetAll().Count();
			}
		}

		public virtual CollectionApi<TApi> GetAll(CollectionRequestApi requestApi, Expression<Func<TEntity, object>>? keySelector, params Expression<Func<TEntity, bool>>[] selectors)
		{
			var allSelectors = selectors.Union(GlobalSelectors).ToArray();
			using (var request = Repository.StartRequest(true, allSelectors))
			{
				var count = request.GetAll().Count();

				int skip = requestApi.Skip ?? 0;
				int take = requestApi.Take ?? 10;

				var allQueryable = request.GetAll();

				if (keySelector != null)
				{
					allQueryable = allQueryable.OrderBy(keySelector);
				}

				var dbItems = allQueryable.Skip(skip).Take(take).ToList();

				IList<TApi> result = new List<TApi>();
				foreach (var dbItem in dbItems)
				{
					result.Add(GetModelMapper.MapToApi(dbItem));
				}

				return new CollectionApi<TApi>(count, skip, take, result.Count, result.ToArray());
			}
		}

		public virtual CollectionApi<TApi> GetAll(CollectionRequestApi requestApi, params Expression<Func<TEntity, bool>>[] selectors)
		{
			return GetAll(requestApi, null, selectors.ToArray());
		}

		public virtual TApi? Get(Expression<Func<TEntity, bool>> itemSelector, params Expression<Func<TEntity, bool>>[] selectors)
		{
			var allSelectors = selectors.Union(GlobalSelectors).ToArray();
			using (var request = Repository.StartRequest(true, allSelectors))
			{
				var entity = request.Get(itemSelector);

				if (entity == null)
				{
					return null;
				}

				return GetModelMapper.MapToApi(entity);
			}
		}

		public virtual TApi Add(TEditApi apiModel, AdditionalAssignmentsAction<TEntity>? additionalAssignments = null)
		{
			using (var request = Repository.StartRequest(false, GlobalSelectors.ToArray()))
			{
				var entity = new TEntity();
				EditEntityMapper.MapToEntity(apiModel, ref entity, additionalAssignments);
				var entityEntry = request.Add(entity);

				return GetModelMapper.MapToApi(entityEntry);
			}
		}

		public virtual TApi? Edit(Expression<Func<TEntity, bool>> itemSelector, TEditApi apiModel, 
			AdditionalAssignmentsAction<TEntity>? additionalAssignments,
			params Expression<Func<TEntity, bool>>[] selectors)
		{
			var allSelectors = selectors.Union(GlobalSelectors).ToArray();
			using (var request = Repository.StartRequest(false, allSelectors))
			{
				var entity = request.Get(itemSelector);

				if (entity == null)
				{
					return null;
				}

				EditEntityMapper.MapToEntity(apiModel, ref entity, additionalAssignments);

				return GetModelMapper.MapToApi(entity);
			}
		}

		public virtual bool Delete(Expression<Func<TEntity, bool>> itemSelector, params Expression<Func<TEntity, bool>>[] selectors)
		{
			var allSelectors = selectors.Union(GlobalSelectors).ToArray();
			using (var request = Repository.StartRequest(false, allSelectors))
			{
				var entity = request.Get(itemSelector);

				if (entity == null)
				{
					return false;
				}

				request.Delete(entity);

				return true;
			}
		}
	}
}
