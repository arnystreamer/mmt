using System.Net;

namespace Jimx.MMT.API.App
{
	public class StatusCodeException : Exception
	{
		public StatusCodeException(HttpStatusCode statusCode, object response, Type responseType)
		{
			StatusCode = statusCode;
			Response = response;
			ResponseType = responseType;
		}

		public StatusCodeException(HttpStatusCode statusCode)
		{
			StatusCode = statusCode;
			Response = null;
			ResponseType = null;
		}

		public HttpStatusCode StatusCode { get; set; }
		public object? Response { get; }
		public Type? ResponseType { get; }
	}
}
