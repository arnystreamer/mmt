using Jimx.MMT.API.Models.StaticItems;

namespace Jimx.MMT.API.Models.Report
{
	public record ReportBySectionAndCategoryResponseApi(IEnumerable<ReportBySectionAndCategoryResponseItemApi> Items);

	public record ReportBySectionAndCategoryResponseItemApi(SectionApi Section, IEnumerable<ReportByCategoryResponseItemApi> Categories)
	{
		public decimal Amount => Categories?.Sum(c => c.Amount) ?? 0.0m;
	}

	public record ReportByCategoryResponseItemApi(CategoryApi? Category, decimal Amount);
}
