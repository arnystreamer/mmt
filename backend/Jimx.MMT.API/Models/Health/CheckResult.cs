using Jimx.Common.WebApi.Models;

namespace Jimx.MMT.API.Models.Health
{
	public class CheckResult(string environmentName, string databaseCreatedResultMessage, string baseUrl, string? userInfo) 
		: HealthCheckResultApi(environmentName)
	{
		public string DatabaseCreatedResultMessage { get; } = databaseCreatedResultMessage;
		public string BaseUrl { get; } = baseUrl;
		public string? UserInfo { get; } = userInfo;
	}
}
