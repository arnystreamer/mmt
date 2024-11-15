﻿using Jimx.MMT.API.Models.Common;
using Jimx.MMT.API.Services.DbWrapper;
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
		protected Expression<Func<TEntity, bool>> GlobalSelector { get; private set; } = x => true;

		public DbActionsWrapper(IRepository<TEntity> repository, 
			IGetModelMapper<TEntity, TApi> getModelMapper,
			IEditEntityMapper<TEntity, TEditApi> editEntityMapper)
		{
			Repository = repository;
			GetModelMapper = getModelMapper;
			EditEntityMapper = editEntityMapper;
		}

		public void SetGlobalCondition(Expression<Func<TEntity, bool>> selector)
		{
			GlobalSelector = selector;
		}

		public virtual int Count(Expression<Func<TEntity, bool>>? selector = null)
		{
			using (var request = Repository.StartRequest(true, GlobalSelector, selector))
			{
				return request.GetAll().Count();
			}
		}

		public virtual CollectionApi<TApi> GetAll(CollectionRequestApi requestApi, Expression<Func<TEntity, bool>>? selector = null)
		{
			using (var request = Repository.StartRequest(true, GlobalSelector, selector))
			{
				var count = request.GetAll().Count();

				int skip = requestApi.Skip ?? 0;
				int take = requestApi.Take ?? 10;
				var dbItems = request.GetAll().Skip(skip).Take(take).ToList();

				IList<TApi> result = new List<TApi>();
				foreach (var dbItem in dbItems)
				{
					result.Add(GetModelMapper.MapToApi(dbItem));
				}

				return new CollectionApi<TApi>(count, skip, take, result.Count, result.ToArray());
			}
		}

		public virtual TApi? Get(Expression<Func<TEntity, bool>> itemSelector, Expression<Func<TEntity, bool>>? selector = null)
		{
			using (var request = Repository.StartRequest(true, GlobalSelector, selector))
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
			using (var request = Repository.StartRequest(false, GlobalSelector))
			{
				var entity = new TEntity();
				EditEntityMapper.MapToEntity(apiModel, ref entity, additionalAssignments);
				var entityEntry = request.Add(entity);

				return GetModelMapper.MapToApi(entityEntry);
			}
		}

		public virtual TApi? Edit(Expression<Func<TEntity, bool>> itemSelector, TEditApi apiModel, Expression<Func<TEntity, bool>>? selector = null, AdditionalAssignmentsAction<TEntity>? additionalAssignments = null)
		{
			using (var request = Repository.StartRequest(false, GlobalSelector))
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

		public virtual bool Delete(Expression<Func<TEntity, bool>> itemSelector, Expression<Func<TEntity, bool>>? selector = null)
		{
			using (var request = Repository.StartRequest(false, GlobalSelector, selector))
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