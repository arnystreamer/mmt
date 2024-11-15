using Jimx.MMT.API.Context;
using Jimx.MMT.API.Services.DbWrapper;

namespace Jimx.MMT.API.Models.StaticItems;

public class LocalSectionModelMapper : IModelMapper<LocalSectionApi, SectionEditApi, Section>
{
	public LocalSectionApi MapToApi(Section entity)
	{
		if (!entity.UserId.HasValue)
		{
			throw new ArgumentException("entity.UserId is null", nameof(entity));
		}

		return new LocalSectionApi(entity.Id, entity.UserId.Value, entity.Name, entity.Description);
	}

	public void MapToEntity(SectionEditApi editApi, ref Section entity, AdditionalAssignmentsAction<Section>? additionalAssignments = null)
	{
		entity.WalletId = null;
		entity.SharedAccountId = null;

		entity.Name = editApi.Name;
		entity.Description = editApi.Description;

		additionalAssignments?.Invoke(ref entity);

		if (!entity.UserId.HasValue)
		{
			throw new InvalidOperationException("entity.UserId must be assigned for local section");
		}

		if (entity.WalletId != null || entity.SharedAccountId != null)
		{
			throw new InvalidOperationException("entity.WalletId and entity.SharedAccountId must be null for local section");
		}
	}
}