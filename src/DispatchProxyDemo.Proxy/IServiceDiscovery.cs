using System;

namespace DispatchProxyDemo.Proxy;

public interface IServiceDiscovery
{
    Uri Resolve(string serviceName);
}
