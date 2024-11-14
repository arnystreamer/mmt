using Jimx.MMT.API.Context;
using Jimx.MMT.API.Services.DbWrapper;

namespace Jimx.MMT.API.Models.StaticItems;

public class GlobalSectionModelMapper : IModelMapper<GlobalSectionApi, SectionEditApi, Section>
{
	public GlobalSectionApi MapToApi(Section entity)
	{
		return new GlobalSectionApi(entity.Id, entity.Name, entity.Description);
	}

	public void MapToEntity(SectionEditApi editApi, ref Section entity, AdditionalAssignmentsAction<Section>? additionalAssignments = null)
	{
		entity.WalletId = null;
		entity.SharedAccountId = null;
		entity.UserId = null;

		entity.Name = editApi.Name;
		entity.Description = editApi.Description;

		additionalAssignments?.Invoke(ref entity);
	}
}

