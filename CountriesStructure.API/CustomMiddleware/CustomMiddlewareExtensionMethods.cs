using System.Runtime.CompilerServices;

namespace CountriesStructure.API.CustomMiddleware
{
    public static class CustomMiddlewareExtensionMethods
    {
        public static IApplicationBuilder UseErrorHandlingMiddleWare(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
