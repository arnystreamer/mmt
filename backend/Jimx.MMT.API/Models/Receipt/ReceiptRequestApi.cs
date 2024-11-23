namespace Jimx.MMT.API.Models.Receipt
{
	public record ReceiptRequestApi(DateTime? DateFrom, DateTime? DateTo, int? LocationId, int? CurrencyId, decimal? SumFrom, decimal? SumTo, string? Comment);
}
