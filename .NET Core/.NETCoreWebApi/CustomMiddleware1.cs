using Microsoft.AspNetCore.Http;

namespace ConsoleToWebApi
{
    public class CustomMiddleware1 : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("Hello from custom middleware \n");
            await next(context);
            await context.Response.WriteAsync("hello form custom middleware 2\n");
        }
    }
}
