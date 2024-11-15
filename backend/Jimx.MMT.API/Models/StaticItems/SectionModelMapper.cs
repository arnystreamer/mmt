using Jimx.MMT.API.Context;
using Jimx.MMT.API.Services.DbWrapper;

namespace Jimx.MMT.API.Models.StaticItems;

public class SectionModelMapper : IModelMapper<SectionApi, SectionEditApi, Section>
{
	public SectionApi MapToApi(Section entity)
	{
		SectionType sectionType =
			(entity.UserId == null, entity.WalletId == null, entity.SharedAccountId == null) switch
			{
				(true, false, false) => SectionType.Local,
				(false, true, false) => SectionType.Wallet,
				(false, false, true) => SectionType.SharedAccount,
				(true, true, true) => SectionType.Global,
				_ => throw new ArgumentException("Invalid combination of (UserId, WalletId, SharedAccountId)", nameof(entity))
			};

		return new SectionApi(entity.Id, sectionType, entity.Name, entity.Description);
	}

	public void MapToEntity(SectionEditApi editApi, ref Section entity, AdditionalAssignmentsAction<Section>? additionalAssignments = null)
	{
		entity.Name = editApi.Name;
		entity.Description = editApi.Description;

		additionalAssignments?.Invoke(ref entity);
	}
}
