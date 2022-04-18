namespace CountriesStructure.API.CustomMiddleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
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


        private static async Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            context.Response.ContentType = "application/json";
            var statusCode = context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var message = e?.Message ?? "something went wrong";

            await context.Response.WriteAsync(new ErrorDescriptor(statusCode, message).ToString());
        }
    }
}
