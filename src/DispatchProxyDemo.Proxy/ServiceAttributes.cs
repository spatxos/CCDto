using System;

namespace DispatchProxyDemo.Proxy;

[AttributeUsage(AttributeTargets.Interface)]
public sealed class ServiceNameAttribute : Attribute
{
    public string Name { get; }
    public ServiceNameAttribute(string name) => Name = name;
}

[AttributeUsage(AttributeTargets.Method)]
public sealed class RouteAttribute : Attribute
{
    public string Template { get; }
    public RouteAttribute(string template) => Template = template;
}
