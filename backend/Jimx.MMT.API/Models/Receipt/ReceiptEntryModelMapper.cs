using Jimx.MMT.API.Context;
using Jimx.MMT.API.Models.StaticItems;
using Jimx.MMT.API.Services.DbWrapper;

namespace Jimx.MMT.API.Models.Receipt;

public class ReceiptEntryModelMapper : IModelMapper<ReceiptEntryApi, ReceiptEntryEditApi, ReceiptEntry>
{
	public ReceiptEntryApi MapToApi(ReceiptEntry entity)
	{
		return new ReceiptEntryApi(entity.Id, entity.ProductId, entity.Quantity, entity.Price, entity.CreateTime, entity.CreateUserId,
			entity.CreateUser != null ? new UserApi(entity.CreateUser.Id, entity.CreateUser.Login, entity.CreateUser.Name) : null);
	}

	public void MapToEntity(ReceiptEntryEditApi editApi, ref ReceiptEntry entity, AdditionalAssignmentsAction<ReceiptEntry>? additionalAssignments = null)
	{
		entity.ProductId = entity.ProductId;
		entity.Quantity = entity.Quantity;
		entity.Price = entity.Price;

		additionalAssignments?.Invoke(ref entity);
	}
}
