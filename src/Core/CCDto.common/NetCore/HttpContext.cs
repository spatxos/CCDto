using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCDto.common.NetCore
{
    public static class HttpContext
    {
        public static IHttpContextAccessor _accessor;
        public static Microsoft.AspNetCore.Http.HttpContext Current => _accessor.HttpContext;
        public static void Configure(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
    }
}
