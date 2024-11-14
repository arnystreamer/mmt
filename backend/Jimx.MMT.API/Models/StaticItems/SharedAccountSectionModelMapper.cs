using Jimx.MMT.API.Context;
using Jimx.MMT.API.Services.DbWrapper;

namespace Jimx.MMT.API.Models.StaticItems;

public class SharedAccountSectionModelMapper : IModelMapper<SharedAccountSectionApi, SectionEditApi, Section>
{
	public SharedAccountSectionApi MapToApi(Section entity)
	{
		if (!entity.SharedAccountId.HasValue)
		{
			throw new ArgumentException("entity.SharedAccountId is null", nameof(entity));
		}

		return new SharedAccountSectionApi(entity.Id, entity.SharedAccountId.Value, entity.Name, entity.Description);
	}

	public void MapToEntity(SectionEditApi editApi, ref Section entity, AdditionalAssignmentsAction<Section>? additionalAssignments = null)
	{
		entity.WalletId = null;
		entity.UserId = null;

		entity.Name = editApi.Name;
		entity.Description = editApi.Description;

		additionalAssignments?.Invoke(ref entity);
	}
}
