using System;
using System.Collections.Concurrent;

namespace DispatchProxyDemo.Proxy;

public class StaticServiceDiscovery : IServiceDiscovery
{
    private readonly ConcurrentDictionary<string, Uri> _map = new();

    public StaticServiceDiscovery Add(string name, Uri baseAddress)
    {
        _map[name] = baseAddress;
        return this;
    }

    public Uri Resolve(string serviceName)
    {
        if (_map.TryGetValue(serviceName, out var uri)) return uri;
        throw new InvalidOperationException("service not found: " + serviceName);
    }
}
