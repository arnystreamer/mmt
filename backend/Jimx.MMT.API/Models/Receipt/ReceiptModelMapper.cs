using Jimx.MMT.API.Models.StaticItems;
using Jimx.MMT.API.Services.DbWrapper;

namespace Jimx.MMT.API.Models.Receipt;

public class ReceiptModelMapper : IModelMapper<ReceiptApi, ReceiptEditApi, Context.Receipt>
{
	public ReceiptApi MapToApi(Context.Receipt entity)
	{
		return new ReceiptApi(entity.Id, entity.Date,
			entity.WalletId,
			entity.Wallet != null ? new WalletApi(entity.Wallet.Id, entity.Wallet.UserId, entity.Wallet.Name, entity.Wallet.Description) : null,
			entity.SharedAccountId,
			entity.SharedAccount != null ? new SharedAccountApi(entity.SharedAccount.Id, entity.SharedAccount.Name, entity.SharedAccount.Description) : null,
			entity.LocationId,
			entity.Location != null ? new LocationApi(entity.Location.Id, entity.Location.CountryCode, entity.Location.LocationCode) : null,
			entity.CurrencyId,
			entity.Currency != null ? new CurrencyApi(entity.Currency.Id, entity.Currency.Code, entity.Currency.Name) : null,
			entity.Comment, 
			entity.CreateTime, 
			entity.CreateUserId,
			entity.CreateUser != null ? new UserApi(entity.CreateUser.Id, entity.CreateUser.Login, entity.CreateUser.Name) : null);
	}

	public void MapToEntity(ReceiptEditApi editApi, ref Context.Receipt entity, AdditionalAssignmentsAction<Context.Receipt>? additionalAssignments = null)
	{
		entity.Date = editApi.Date;
		entity.WalletId = editApi.WalletId;
		entity.SharedAccountId = editApi.SharedAccountId;
		entity.LocationId = editApi.LocationId;
		entity.CurrencyId = editApi.CurrencyId;
		entity.Comment = editApi.Comment;

		additionalAssignments?.Invoke(ref entity);

		if (entity.SharedAccountId != null && entity.WalletId != null)
		{
			throw new InvalidOperationException("Receipt has SharedAccountId and WalletId set at the same time");
		}
	}
}
