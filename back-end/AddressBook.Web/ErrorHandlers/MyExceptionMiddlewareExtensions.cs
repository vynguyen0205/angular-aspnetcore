using Microsoft.AspNetCore.Builder;

namespace AddressBook.Web.ErrorHandlers
{
    public static class MyExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpStatusCodeExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyExceptionMiddleware>();
        }
    }
}
