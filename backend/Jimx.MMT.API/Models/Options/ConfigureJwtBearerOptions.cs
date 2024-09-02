using Jimx.MMT.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Jimx.MMT.API.Models.Options
{
	public class ConfigureJwtBearerOptions : IConfigureNamedOptions<JwtBearerOptions>
	{
		private readonly SettingsProvider _settingsProvider;
		private readonly KeysProvider _keysProvider;

		public ConfigureJwtBearerOptions(SettingsProvider settingsProvider, KeysProvider keysProvider)
		{
			_settingsProvider = settingsProvider;
			_keysProvider = keysProvider;
		}

		public void Configure(string? name, JwtBearerOptions options)
		{
			Configure(options);
		}

		public void Configure(JwtBearerOptions options)
		{
			var bytes = Encoding.UTF8.GetBytes(_keysProvider.CredentialsSigningKey);

			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,

				ValidIssuer = _settingsProvider.AuthIssuer,
				ValidAudience = _settingsProvider.BaseUrl,
				IssuerSigningKey = new SymmetricSecurityKey(bytes)
			};
		}
	}
}
