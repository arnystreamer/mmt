namespace Jimx.MMT.API.App
{
	public class StatusCodeExceptionHandler
	{
		private readonly RequestDelegate request;

		public StatusCodeExceptionHandler(RequestDelegate pipeline)
		{
			request = pipeline;
		}

		public Task Invoke(HttpContext context) => InvokeAsync(context);

		async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await request(context);
			}
			catch (StatusCodeException exception)
			{
				context.Response.StatusCode = (int)exception.StatusCode;
				if (exception.Response != null && exception.ResponseType != null)
				{
					await context.Response.WriteAsJsonAsync(exception.Response, exception.ResponseType);
				}
			}
		}
	}
}
