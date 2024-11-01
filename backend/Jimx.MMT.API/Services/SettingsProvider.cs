using Jimx.MMT.API.Models.Options;
using Microsoft.Extensions.Options;

namespace Jimx.MMT.API.Services
{
	public class SettingsProvider
	{
		public string AuthIssuer { get; init; }
		public string BaseUrl { get; init; }
		public string FrontendUrl { get; init; }

		public SettingsProvider(IOptions<GeneralOptions> options)
		{
			AuthIssuer = (!string.IsNullOrWhiteSpace(options.Value.Auth?.Issuer) ? options.Value.Auth.Issuer :
				Environment.GetEnvironmentVariable("GENERAL_AUTH_ISSUER"))
				?? "MMT.API";

			BaseUrl = (!string.IsNullOrWhiteSpace(options.Value.BaseUrl) ? options.Value.BaseUrl :
				Environment.GetEnvironmentVariable("GENERAL_BASEURL"))
				?? "https://localhost:55562";

			FrontendUrl = (!string.IsNullOrWhiteSpace(options.Value.FrontendUrl) ? options.Value.FrontendUrl :
				Environment.GetEnvironmentVariable("GENERAL_FRONTENDURL"))
				?? "https://localhost:4200";
		}
	}
}
