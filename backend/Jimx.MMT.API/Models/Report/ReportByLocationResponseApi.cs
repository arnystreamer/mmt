using Jimx.MMT.API.Models.StaticItems;

namespace Jimx.MMT.API.Models.Report
{
	public record ReportByLocationResponseApi(IEnumerable<ReportByLocationResponseCountryApi> Items);

	public record ReportByLocationResponseCountryApi(string CountryCode, IEnumerable<ReportByLocationResponseItemApi> Items)
	{
		public decimal Amount => Items?.Sum(x => x.Amount) ?? 0.0m;
	}

	public record ReportByLocationResponseItemApi(LocationApi Location, decimal Amount);
}
