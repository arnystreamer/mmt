using Jimx.MMT.API.Context;
using Jimx.MMT.API.Models.Report;
using Jimx.MMT.API.Models.StaticItems;
using Jimx.MMT.API.Services.Finance;
using Microsoft.AspNetCore.Mvc;

namespace Jimx.MMT.API.Controllers
{
	[Route("report")]
	[ApiController]
	public class ReportController : ControllerBase
	{
		private readonly ILogger<ReportController> _logger;
		private readonly ApiDbContext _context;
		private readonly CrossCurrencyService _crossCurrencyService;

		public ReportController(ILogger<ReportController> logger, ApiDbContext context, CrossCurrencyService crossCurrencyService)
		{
			_logger = logger;
			_context = context;
			_crossCurrencyService = crossCurrencyService;
		}

		[HttpGet("by-section")]
		public ReportBySectionResponseApi GetBySection([FromQuery] ReportRequestApi requestApi)
		{
			var reportItems = GetReportItems(requestApi);

			var sectionMapper = new SectionModelMapper();

			var itemsBySection = reportItems
				.GroupBy(ri => ri.Section)
				.Select(sg => new ReportBySectionResponseItemApi(
					sectionMapper.MapToApi(sg.Key),
					sg.Sum(sgi => _crossCurrencyService.GetRate(sgi.Receipt.Date, sgi.Currency.Code, "EUR") * sgi.ReceiptEntry.Quantity * sgi.ReceiptEntry.Price)
					))
				.OrderByDescending(i => i.Amount);

			return new ReportBySectionResponseApi(itemsBySection);
		}

		[HttpGet("by-section-and-category")]
		public ReportBySectionAndCategoryResponseApi GetBySectionAndCategory([FromQuery] ReportRequestApi requestApi)
		{
			var reportItems = GetReportItems(requestApi);

			var sectionMapper = new SectionModelMapper();
			var categoryMapper = new CategoryModelMapper();

			var itemsBySectionAndCategory = reportItems
				.GroupBy(ri => ri.Section)
				.Select(sg => new ReportBySectionAndCategoryResponseItemApi(
					sectionMapper.MapToApi(sg.Key),
					sg.GroupBy(sgi => sgi.Category)
						.Select(scg => new ReportByCategoryResponseItemApi(
							scg.Key != null ? categoryMapper.MapToApi(scg.Key) : null,
							scg.Sum(scgi => _crossCurrencyService.GetRate(scgi.Receipt.Date, scgi.Currency.Code, "EUR") * scgi.ReceiptEntry.Quantity * scgi.ReceiptEntry.Price)
							))
						.OrderByDescending(scg => scg.Amount)
					))
				.OrderByDescending(ri => ri.Amount)
				.ToList();

			return new ReportBySectionAndCategoryResponseApi(itemsBySectionAndCategory);
		}

		[HttpGet("by-month")]
		public ReportByMonthResponseApi GetByMonth([FromQuery] ReportRequestApi requestApi)
		{
			var reportItems = GetReportItems(requestApi);

			var itemsByMonth = reportItems
				.GroupBy(ri => new { ri.Receipt.Date.Year, ri.Receipt.Date.Month })
				.Select(ri => new ReportByMonthResponseItemApi(
					ri.Key.Year, 
					ri.Key.Month,
					ri.Sum(i => _crossCurrencyService.GetRate(i.Receipt.Date, i.Currency.Code, "EUR") * i.ReceiptEntry.Quantity * i.ReceiptEntry.Price)
					))
				.OrderByDescending(ri => ri.Year).ThenByDescending(ri => ri.Month);

			return new ReportByMonthResponseApi(itemsByMonth);
		}

		[HttpGet("by-location")]
		public ReportByLocationResponseApi GetByLocation([FromQuery] ReportRequestApi requestApi)
		{
			var reportItems = GetReportItems(requestApi);

			var locationMapper = new LocationModelMapper();

			var itemsByLocation = reportItems
				.GroupBy(ri => ri.Location.CountryCode)
				.Select(ri => new ReportByLocationResponseCountryApi(
					ri.Key,
					ri.GroupBy(rci => rci.Location)
						.Select(rci => new ReportByLocationResponseItemApi(
							locationMapper.MapToApi(rci.Key),
							rci.Sum(i => _crossCurrencyService.GetRate(i.Receipt.Date, i.Currency.Code, "EUR") * i.ReceiptEntry.Quantity * i.ReceiptEntry.Price)
						))
						.OrderByDescending(rci => rci.Amount)
				))
				.OrderByDescending(ri => ri.CountryCode);

			return new ReportByLocationResponseApi(itemsByLocation);
		}

		private IList<ReportItem> GetReportItems(ReportRequestApi requestApi)
		{
			var reportItems =
				(from re in _context.ReceiptEntries
				 where (!requestApi.StartDate.HasValue || re.Receipt.Date >= requestApi.StartDate.Value.ToUniversalTime().Date)
								 && (!requestApi.EndDate.HasValue || re.Receipt.Date <= requestApi.EndDate.Value.ToUniversalTime().Date)
				 select new ReportItem(
					 re,
					 re.Receipt,
					 re.Product.Section,
					 re.Product.Category,
					 re.Receipt.Currency,
					 re.Receipt.Location
				 )).ToList();

			return reportItems;
		}


		private record ReportItem(ReceiptEntry ReceiptEntry, Receipt Receipt, Section Section, Category? Category, Currency Currency, Location Location);
	}

	
}
