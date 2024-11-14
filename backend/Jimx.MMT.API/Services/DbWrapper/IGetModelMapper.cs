namespace Jimx.MMT.API.Services.DbWrapper
{
	public interface IGetModelMapper<TEntity, TApi>
		where TEntity : class
	{
		TApi MapToApi(TEntity entity);
	}
}
