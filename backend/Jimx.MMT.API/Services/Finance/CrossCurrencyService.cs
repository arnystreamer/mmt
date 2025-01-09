namespace Jimx.MMT.API.Services.Finance
{
	public class CrossCurrencyService
	{
		private enum GlobalCurrencyDirection
		{
			From,
			To
		}

		private string GlobalCurrencyCode = "EUR";

		private (DateTime EndDate, string CurrencyCode, GlobalCurrencyDirection GlobalCurrencyDirection, decimal Rate)[] rates =
			new[]
			{
				(new DateTime(2026, 1, 1), "USD", GlobalCurrencyDirection.From, 1.03157m),
				(new DateTime(2026, 1, 1), "USD", GlobalCurrencyDirection.To, 1.0m/1.03157m),
				(new DateTime(2026, 1, 1), "CHF", GlobalCurrencyDirection.From, 0.94029m),
				(new DateTime(2026, 1, 1), "CHF", GlobalCurrencyDirection.To, 1.0m/0.94029m),
				(new DateTime(2026, 1, 1), "RUB", GlobalCurrencyDirection.From, 105.0m),
				(new DateTime(2026, 1, 1), "RUB", GlobalCurrencyDirection.To, 1.0m/105.0m)
			};

		public decimal GetRate(DateTime date, string SourceCurrencyCode, string DestinationCurrencyCode)
		{
			try
			{
				if (SourceCurrencyCode == DestinationCurrencyCode)
				{
					return 1.0m;
				}

				if (SourceCurrencyCode == GlobalCurrencyCode)
				{
					var rate = rates.Where(r => r.GlobalCurrencyDirection == GlobalCurrencyDirection.From
						&& r.CurrencyCode == DestinationCurrencyCode
						&& r.EndDate >= DateTime.Now)
						.OrderBy(r => r.EndDate)
						.First()
						.Rate;

					return rate;
				}

				if (DestinationCurrencyCode == GlobalCurrencyCode)
				{
					var rate = rates.Where(r => r.GlobalCurrencyDirection == GlobalCurrencyDirection.To
						&& r.CurrencyCode == SourceCurrencyCode
						&& r.EndDate >= DateTime.Now)
						.OrderBy(r => r.EndDate)
						.First()
						.Rate;

					return rate;
				}

				return GetRate(date, SourceCurrencyCode, GlobalCurrencyCode) * GetRate(date, GlobalCurrencyCode, DestinationCurrencyCode);
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException($"{date:O} {SourceCurrencyCode} {DestinationCurrencyCode}", ex);
			}
		}
	}
}
