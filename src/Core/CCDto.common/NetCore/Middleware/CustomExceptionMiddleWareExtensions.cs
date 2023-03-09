using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCDto.common.NetCore.Middleware
{
    public static class CustomExceptionMiddleWareExtensions
    {

        public static IApplicationBuilder UseCustomException(this IApplicationBuilder app, CustomExceptionMiddleWareOption option)
        {
            return app.UseMiddleware<CustomExceptionMiddleWare>(option);
        }
    }
}
