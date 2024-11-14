namespace Jimx.MMT.API.Models.Receipt;

public record ReceiptEditApi(DateTime Date, int LocationId, int CurrencyId, string? Comment);

