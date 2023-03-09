using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCDto.common.NetCore;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CCDto.common.NetCore.Extensions
{
    public static class StaticHttpContextExtensions
    {
        public static void AddCurrentHttpContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public static IApplicationBuilder UseStaticHttpContext(this IApplicationBuilder app)
        {
            var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            CCDto.common.NetCore.CurrentHttpContext.Configure(httpContextAccessor);
            return app;
        }

        /// <summary>
        /// action没有httpmethod attribute的情况下根据action的开头名称给与默认值
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <param name="defaultHttpMethod">默认给定的HttpMethod</param>
        public static IApplicationBuilder AutoHttpMethodIfActionNoBind(this IApplicationBuilder app, string defaultHttpMethod = null)
        {
            
            //从容器中获取IApiDescriptionGroupCollectionProvider实例
            var apiDescriptionGroupCollectionProvider = app.ApplicationServices.GetRequiredService<IApiDescriptionGroupCollectionProvider>();
            var apiDescriptionGroupsItems = apiDescriptionGroupCollectionProvider.ApiDescriptionGroups.Items;
            //遍历ApiDescriptionGroups
            foreach (var apiDescriptionGroup in apiDescriptionGroupsItems)
            {
                foreach (var apiDescription in apiDescriptionGroup.Items)
                {
                    if (string.IsNullOrEmpty(apiDescription.HttpMethod))
                    {
                        //获取Action名称
                        var actionName = apiDescription.ActionDescriptor.RouteValues["action"];
                        //默认给定POST
                        string methodName = defaultHttpMethod ?? "POST";
                        //根据Action开头单词给定HttpMethod默认值
                        if (actionName.StartsWith("get", StringComparison.OrdinalIgnoreCase))
                        {
                            methodName = "GET";
                        }
                        else if (actionName.StartsWith("put", StringComparison.OrdinalIgnoreCase))
                        {
                            methodName = "PUT";
                        }
                        else if (actionName.StartsWith("delete", StringComparison.OrdinalIgnoreCase))
                        {
                            methodName = "DELETE";
                        }
                        apiDescription.HttpMethod = methodName;
                    }
                }
            }
            return app;
        }
    }
}
