using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DispatchProxyDemo.Proxy;
using DispatchProxyDemo.Proxy.Interfaces;
using DispatchProxyDemo.Proxy.DTOs;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5201");

builder.Services.AddHttpClient();
var baseUrl = Environment.GetEnvironmentVariable("DBCONNECTION_API") ?? "http://localhost:5101";
builder.Services.AddSingleton<IServiceDiscovery>(sp => new StaticServiceDiscovery()
    .Add("dbconnection", new Uri(baseUrl)));
builder.Services.AddHttpServiceProxy<IDBConnectionService>();
builder.Services.AddHttpServiceProxy<IDBTableService>();
builder.Services.AddHttpServiceProxy<IDBFieldService>();

var app = builder.Build();

app.MapGet("/connections", async (IDBConnectionService svc) => await svc.GetConnections());

app.MapGet("/aggregate/{connection}", async (string connection, IDBTableService tableSvc, IDBFieldService fieldSvc) =>
{
    var tables = await tableSvc.GetTables(connection);
    foreach (var t in tables)
    {
        var fields = await fieldSvc.GetFields(connection, t.Name);
        t.Fields = fields;
    }
    var result = new DBConnectionDto { Name = connection, Tables = tables };
    return result;
});

app.Run();
