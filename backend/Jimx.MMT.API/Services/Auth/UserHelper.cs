using Jimx.MMT.API.Context;
using System.Security.Claims;
using System.Text;

namespace Jimx.MMT.API.Services.Auth
{
	public static class UserHelper
	{
		public static string? GetInfo(this ClaimsPrincipal? principal, bool includeClaims = false)
		{
			if (principal?.Identity == null)
			{
				return null;
			}

			if (principal.Identity.IsAuthenticated == false)
			{
				return "Unauthenticated";
			}

			var identity = principal.Identity;
			var info = $"{identity.AuthenticationType ?? "None"}: {identity.Name ?? "No name"}";

			if (includeClaims)
			{
				info += " (" + principal.Claims
					.Aggregate(new StringBuilder(), (sb, next) => sb.Append($"{next.Type}: {next.Value}, "))
					.ToString() + ")";
			}

			return info;
		}

		public static string GetRequiredUserLoginLowered(this ClaimsPrincipal? principal)
		{
			if (principal?.Identity == null)
			{
				throw new InvalidOperationException("Cannot get login when user is not set");
			}

			if (principal.Identity.IsAuthenticated == false)
			{
				throw new InvalidOperationException("Unauthenticated");
			}

			if (string.IsNullOrEmpty(principal.Identity.Name))
			{
				throw new InvalidOperationException("User exists, but login is empty");
			}

			return principal.Identity.Name.ToLower();
		}

		public static User GetCurrentUserFromContext(this IQueryable<User> users, ClaimsPrincipal principal)
		{
			var currentLogin = GetRequiredUserLoginLowered(principal);
			var currentUser = users.FirstOrDefault(u => u.Login.ToLower() == currentLogin);

			if (currentUser == null)
			{
				throw new InvalidOperationException("User not found in collection");
			}

			return currentUser;
		}
	}
}
