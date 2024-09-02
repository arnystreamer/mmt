using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Jimx.MMT.API.Models.Login;
using Jimx.MMT.API.Services;

namespace Jimx.MMT.API.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly SettingsProvider _settingsProvider;
		private readonly KeysProvider _keysProvider;

		public LoginController(SettingsProvider settingsProvider, KeysProvider keysProvider) 
		{
			_settingsProvider = settingsProvider;
			_keysProvider = keysProvider;
		}


		[HttpPost]
		public IActionResult Auth(LoginModel model)
		{
			//#error https://medium.com/@mbektas0506/implementing-a-simple-authentication-mechanism-in-asp-net-core-step-by-step-0d27c9dfc60f
			//			this.HttpContent.

			if (model.Login == "user" && model.Password == "secret")
			{
				var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_keysProvider.CredentialsSigningKey));
				var loginCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
				var tokenOptions = new JwtSecurityToken(
					issuer: _settingsProvider.AuthIssuer,
					audience: _settingsProvider.BaseUrl,
					claims: new List<Claim>(),
					expires: DateTime.Now.AddMinutes(30),
					signingCredentials: loginCredentials
				);
				var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
				return Ok(new { Token = tokenString });
			}
			else
			{
				return Unauthorized();
			}
		}
	}
}
