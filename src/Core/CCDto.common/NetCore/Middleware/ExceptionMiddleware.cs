using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCDto.common.NetCore.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            Console.WriteLine(httpContext.Request.Host.ToString() + httpContext.Request.Path + httpContext.Request.QueryString);
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                httpContext.Response.StatusCode = 200;
                httpContext.Response.ContentType = "application/json; charset=utf-8";
                Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                await httpContext.Response.WriteAsync(ex.Message + "\r\n" + ex.StackTrace);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
