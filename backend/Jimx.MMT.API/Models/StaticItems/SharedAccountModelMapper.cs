using Jimx.MMT.API.Context;
using Jimx.MMT.API.Services.DbWrapper;

namespace Jimx.MMT.API.Models.StaticItems;

public class SharedAccountModelMapper : IModelMapper<SharedAccountApi, SharedAccountEditApi, SharedAccount>
{
	public SharedAccountApi MapToApi(SharedAccount entity)
	{
		return new SharedAccountApi(entity.Id, entity.Name, entity.Description);
	}

	public void MapToEntity(SharedAccountEditApi editApi, ref SharedAccount entity, AdditionalAssignmentsAction<SharedAccount>? additionalAssignments = null)
	{
		entity.Name = editApi.Name;
		entity.Description = editApi.Description;

		additionalAssignments?.Invoke(ref entity);
	}
}
