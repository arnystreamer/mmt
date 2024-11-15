namespace Jimx.MMT.API.Services.DbWrapper
{
	public static class ProxyGetModelMapperHelper
	{
		public static IGetModelMapper<TEntity, TApi> Create<TEntity, TApi>(Func<TEntity, TApi> func)
			where TEntity : class
		{
			return new ProxyGetModelMapper<TEntity, TApi>(func);
		}
	}
}
