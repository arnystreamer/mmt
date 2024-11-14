namespace Jimx.MMT.API.Services.DbWrapper
{
	public class ProxyEditEntityMapper<TEntity, TEdit> : IEditEntityMapper<TEntity, TEdit>
		where TEntity : class
	{
		private readonly Action<TEdit, TEntity> _func;

		public ProxyEditEntityMapper(Action<TEdit, TEntity> func) 
		{
			_func = func;
		}

		public void MapToEntity(TEdit editApi, ref TEntity entity, AdditionalAssignmentsAction<TEntity>? additionalAssignments = null)
		{
			_func(editApi, entity);
			additionalAssignments?.Invoke(ref entity);
		}
	}
}
