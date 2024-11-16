namespace Jimx.MMT.API.Models.Receipt;

public record ReceiptEditApi(DateTime Date, int? WalletId, int? SharedAccountId, int LocationId, int CurrencyId, 
	string? Comment, DateTime CreateTime);

