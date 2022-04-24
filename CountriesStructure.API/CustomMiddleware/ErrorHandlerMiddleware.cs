namespace CountriesStructure.API.CustomMiddleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _host;

        public ErrorHandlerMiddleware(RequestDelegate next, IWebHostEnvironment host)
        {
            _next = next;
            _host = host;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }


        private async Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            
            var statusCode = context.Response.StatusCode = context.Response.StatusCode;
            var message = e.Message ?? "something went wrong";
            var stackTrace = _host.EnvironmentName.Equals("Development") ? e?.StackTrace : null;

            await context.Response.WriteAsync(new ErrorDescriptor(statusCode, message, stackTrace).ToString());
        }
    }
}
