using Jimx.MMT.API.Context;
using Jimx.MMT.API.Services.DbWrapper;

namespace Jimx.MMT.API.Models.StaticItems;

public class UserModelMapper : IModelMapper<UserApi, UserEditApi, User>
{
	public UserApi MapToApi(User entity)
	{
		return new UserApi(entity.Id, entity.Login, entity.Name);
	}

	public void MapToEntity(UserEditApi editApi, ref User entity, AdditionalAssignmentsAction<User>? additionalAssignments = null)
	{
		entity.Login = editApi.Login;
		entity.Name = editApi.Name;

		additionalAssignments?.Invoke(ref entity);
	}
}