namespace Jimx.MMT.API.Services.DbWrapper
{
	public class ProxyGetModelMapper<TEntity, TApi> : IGetModelMapper<TEntity, TApi>
		where TEntity : class
	{
		private readonly Func<TEntity, TApi> _func;

		public ProxyGetModelMapper(Func<TEntity, TApi> mapFunc)
		{
			_func = mapFunc;
		}

		public TApi MapToApi(TEntity entity)
		{
			return _func(entity);
		}
	}
}
