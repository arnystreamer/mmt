using Jimx.MMT.API.Models.StaticItems;

namespace Jimx.MMT.API.Models.Report
{
	public record ReportBySectionResponseApi(IEnumerable<ReportBySectionResponseItemApi> Items);

	public record ReportBySectionResponseItemApi(SectionApi Section, decimal Amount);	
}
