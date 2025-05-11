using Jimx.MMT.API.Models.StaticItems;

namespace Jimx.MMT.API.Models.Report
{
	public record ReportByMonthResponseApi(IEnumerable<ReportByMonthResponseItemApi> Items);

	public record ReportByMonthResponseItemApi(int Year, int Month, decimal Amount);
}
