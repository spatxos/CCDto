using System;
using System.Reflection;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace DispatchProxyDemo.Proxy;

public static class ProxyRegistrationExtensions
{
    public static IServiceCollection AddHttpServiceProxy<TService>(this IServiceCollection services)
        where TService : class
    {
        services.AddTransient<TService>(sp =>
        {
            var discovery = sp.GetRequiredService<IServiceDiscovery>();
            var httpFactory = sp.GetRequiredService<IHttpClientFactory>();
            var http = httpFactory.CreateClient(typeof(TService).FullName);
            var proxy = System.Reflection.DispatchProxy.Create<TService, HttpServiceProxy>();
            var sname = typeof(TService).GetCustomAttributes(typeof(ServiceNameAttribute), true);
            var name = sname.Length > 0 ? ((ServiceNameAttribute)sname[0]).Name : typeof(TService).Name;
            var impl = (HttpServiceProxy)(object)proxy;
            impl.HttpClient = http;
            impl.Discovery = discovery;
            impl.ServiceName = name;
            return proxy;
        });
        return services;
    }
}
