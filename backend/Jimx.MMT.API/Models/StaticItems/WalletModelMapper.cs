using Jimx.MMT.API.Context;
using Jimx.MMT.API.Services.DbWrapper;

namespace Jimx.MMT.API.Models.StaticItems;

public class WalletModelMapper : IModelMapper<WalletApi, WalletEditApi, Wallet>
{
	public WalletApi MapToApi(Wallet entity)
	{
		return new WalletApi(entity.Id, entity.UserId, entity.Name, entity.Description);
	}

	public void MapToEntity(WalletEditApi editApi, ref Wallet entity, AdditionalAssignmentsAction<Wallet>? additionalAssignments = null)
	{
		entity.Name = editApi.Name;
		entity.Description = editApi.Description;

		additionalAssignments?.Invoke(ref entity);
	}
}
