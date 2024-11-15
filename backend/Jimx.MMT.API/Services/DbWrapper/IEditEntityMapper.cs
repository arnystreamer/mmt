namespace Jimx.MMT.API.Services.DbWrapper
{
	public delegate void AdditionalAssignmentsAction<TEntity>(ref TEntity entity)
		where TEntity : class;

	public interface IEditEntityMapper<TEntity, TEditApi>
		where TEntity : class
	{
		void MapToEntity(TEditApi editApi, ref TEntity entity, AdditionalAssignmentsAction<TEntity>? additionalAssignments = null);
	}
}
