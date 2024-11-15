using Jimx.MMT.API.Context;
using Jimx.MMT.API.Services.DbWrapper;

namespace Jimx.MMT.API.Models.StaticItems;

public class WalletSectionModelMapper : IModelMapper<WalletSectionApi, SectionEditApi, Section>
{
	public WalletSectionApi MapToApi(Section entity)
	{
		if (!entity.WalletId.HasValue)
		{
			throw new ArgumentException("entity.WalletId is null", nameof(entity));
		}

		return new WalletSectionApi(entity.Id, entity.WalletId.Value, entity.Name, entity.Description);
	}

	public void MapToEntity(SectionEditApi editApi, ref Section entity, AdditionalAssignmentsAction<Section>? additionalAssignments = null)
	{
		entity.UserId = null;
		entity.SharedAccountId = null;

		entity.Name = editApi.Name;
		entity.Description = editApi.Description;

		additionalAssignments?.Invoke(ref entity);

		if (!entity.WalletId.HasValue)
		{
			throw new InvalidOperationException("entity.WalletId must be assigned for wallet section");
		}

		if (entity.UserId != null || entity.SharedAccountId != null)
		{
			throw new InvalidOperationException("entity.UserId and entity.SharedAccountId must be null for wallet section");
		}
	}
}
