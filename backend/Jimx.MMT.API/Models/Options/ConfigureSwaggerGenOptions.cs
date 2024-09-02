using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Jimx.MMT.API.Models.Options
{
	public class ConfigureSwaggerGenOptions : IConfigureNamedOptions<SwaggerGenOptions>
	{
		public void Configure(string? name, SwaggerGenOptions options)
		{
			Configure(options);
		}

		public void Configure(SwaggerGenOptions options)
		{
			options.SwaggerDoc("v1", new OpenApiInfo { Title = "Jimx.MMT.API", Version = "v1" });
			options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
			{
				In = ParameterLocation.Header,
				Description = "Please enter token",
				Name = "Authorization",
				Type = SecuritySchemeType.Http,
				BearerFormat = "JWT",
				Scheme = "bearer"
			});

			options.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type=ReferenceType.SecurityScheme,
							Id="Bearer"
							
						},
					},
					new string[]{}
				}
			});
		}
	}
}
