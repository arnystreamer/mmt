using Jimx.MMT.API.Context;
using Jimx.MMT.API.Services.DbWrapper;

namespace Jimx.MMT.API.Models.StaticItems
{
	public class SharedAccountToUserModelMapper : IModelMapper<SharedAccountToUserApi, SharedAccountToUserEditApi, SharedAccountToUser>
	{
		public SharedAccountToUserApi MapToApi(SharedAccountToUser entity)
		{
			return new SharedAccountToUserApi(entity.Id, entity.SharedAccountId, entity.UserId);
		}

		public void MapToEntity(SharedAccountToUserEditApi editApi, ref SharedAccountToUser entity, AdditionalAssignmentsAction<SharedAccountToUser>? additionalAssignments = null)
		{
			entity.UserId = editApi.UserId;

			additionalAssignments?.Invoke(ref entity);
		}
	}
}
