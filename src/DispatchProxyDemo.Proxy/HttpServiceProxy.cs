using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;

namespace DispatchProxyDemo.Proxy;

public class HttpServiceProxy : DispatchProxy
{
    internal HttpClient HttpClient { get; set; } = default!;
    internal IServiceDiscovery Discovery { get; set; } = default!;
    internal string ServiceName { get; set; } = string.Empty;

    protected override object? Invoke(MethodInfo targetMethod, object?[]? args)
    {
        var route = targetMethod.GetCustomAttribute<RouteAttribute>()?.Template ?? string.Empty;
        var baseUri = Discovery.Resolve(ServiceName).ToString().TrimEnd('/');
        var path = route.TrimStart('/');
        var url = baseUri + "/" + path;

        var parameters = targetMethod.GetParameters();
        if (args != null && args.Length > 0)
        {
            var pairs = parameters.Select((p, i) => new { p.Name, v = args[i] })
                                  .Where(x => x.v != null)
                                  .Select(x => Uri.EscapeDataString(x.Name!) + "=" + Uri.EscapeDataString(x.v!.ToString()!));
            var query = string.Join("&", pairs);
            if (!string.IsNullOrEmpty(query)) url += "?" + query;
        }

        var returnType = targetMethod.ReturnType;
        if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(System.Threading.Tasks.Task<>))
        {
            var t = returnType.GetGenericArguments()[0];
            var m = typeof(HttpServiceProxy).GetMethod(nameof(GetFromJsonGeneric), BindingFlags.Instance | BindingFlags.NonPublic)!
                                            .MakeGenericMethod(t);
            return m.Invoke(this, new object[] { url });
        }
        if (returnType == typeof(System.Threading.Tasks.Task))
        {
            return HttpClient.GetAsync(url);
        }
        throw new NotSupportedException("unsupported return type");
    }

    private System.Threading.Tasks.Task<T> GetFromJsonGeneric<T>(string url)
        => HttpClient.GetFromJsonAsync<T>(url)!;
}
