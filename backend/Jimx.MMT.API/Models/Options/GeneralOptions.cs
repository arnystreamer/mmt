namespace Jimx.MMT.API.Models.Options
{
	public class GeneralOptions
	{
		public const string OptionName = "General";

		public AuthGeneralOptions Auth { get; set; } = new AuthGeneralOptions();
		public string BaseUrl { get; set; } = string.Empty;
	}
}