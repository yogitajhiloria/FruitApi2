namespace FruitApi.Api
{
    using System.Net;
	using System.Text.Json;

	
	public class ExceptionHandlerMiddleware
	{
		private readonly RequestDelegate next;
		private readonly ILogger<ExceptionHandlerMiddleware> logger;

		public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
		{
			this.next = next;
			this.logger = logger;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await next(httpContext);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(httpContext, ex);
			}
		}

		private async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";
			var response = context.Response;

			var errorResponse = new ErrorResponse
			{
				Success = false
			};
			switch (exception)
			{
				case ApplicationException ex:
					if (ex.Message.Contains("Invalid Token"))
					{
						response.StatusCode = (int)HttpStatusCode.Forbidden;
						errorResponse.Message = ex.Message;
						break;
					}
					response.StatusCode = (int)HttpStatusCode.BadRequest;
					errorResponse.Message = ex.Message;
					break;
				default:
					response.StatusCode = (int)HttpStatusCode.InternalServerError;
					errorResponse.Message = "Internal server error!";
					break;
			}
			logger.LogError(exception.Message);
			var result = JsonSerializer.Serialize(errorResponse);
			await context.Response.WriteAsync(result);
		}
	}
}
