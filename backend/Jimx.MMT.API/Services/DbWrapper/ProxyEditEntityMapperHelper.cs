namespace Jimx.MMT.API.Services.DbWrapper
{
	public static class ProxyEditEntityMapperHelper
	{
		public static IEditEntityMapper<TEntity, TEdit> Create<TEntity, TEdit>(Action<TEdit, TEntity> assignFunc)
			where TEntity : class
		{
			return new ProxyEditEntityMapper<TEntity, TEdit>(assignFunc);
		}
	}
}
