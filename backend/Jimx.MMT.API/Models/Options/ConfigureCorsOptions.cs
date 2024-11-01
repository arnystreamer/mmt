using Jimx.MMT.API.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Options;

namespace Jimx.MMT.API.Models.Options
{
	public class ConfigureCorsOptions : IConfigureNamedOptions<CorsOptions>
	{
		private readonly SettingsProvider _settingsProvider;

		public ConfigureCorsOptions(SettingsProvider settingsProvider)
		{
			_settingsProvider = settingsProvider;
		}

		public void Configure(string? name, CorsOptions options)
		{
			Configure(options);
		}

		public void Configure(CorsOptions options)
		{
			options.AddPolicy("Frontend",
			policy =>
			{
				policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
				policy.WithOrigins(_settingsProvider.FrontendUrl).AllowAnyOrigin().AllowAnyHeader();
			});
		}
	}
}
