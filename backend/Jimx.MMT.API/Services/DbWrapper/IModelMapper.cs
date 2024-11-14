namespace Jimx.MMT.API.Services.DbWrapper
{
	public interface IModelMapper<TApi, TEditApi, TEntity> : IGetModelMapper<TEntity, TApi>, IEditEntityMapper<TEntity, TEditApi>
		where TEntity : class
	{

	}
}
