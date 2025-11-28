using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using DispatchProxyDemo.Proxy.DTOs;
using MySqlConnector;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5101");

var app = builder.Build();

string BuildConnString()
{
    var host = Environment.GetEnvironmentVariable("DB_HOST");
    var port = Environment.GetEnvironmentVariable("DB_PORT") ?? "3306";
    var user = Environment.GetEnvironmentVariable("DB_USER");
    var pwd = Environment.GetEnvironmentVariable("DB_PASSWORD");
    if (string.IsNullOrWhiteSpace(host) || string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pwd))
        throw new InvalidOperationException("DB connection env vars required: DB_HOST, DB_USER, DB_PASSWORD");
    return $"Server={host};Port={port};User ID={user};Password={pwd};SslMode=None;AllowPublicKeyRetrieval=True";
}

app.MapGet("/connections", async () =>
{
    var list = new List<DBConnectionDto>();
    await using var conn = new MySqlConnection(BuildConnString());
    await conn.OpenAsync();
    var sql = @"SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA
                WHERE SCHEMA_NAME NOT IN ('information_schema','mysql','performance_schema','sys')";
    await using var cmd = new MySqlCommand(sql, conn);
    await using var reader = await cmd.ExecuteReaderAsync();
    while (await reader.ReadAsync())
    {
        var schema = reader.GetString(0);
        list.Add(new DBConnectionDto { Name = schema });
    }
    return list;
});

app.MapGet("/tables", async (string connectionName) =>
{
    var list = new List<DBTableDto>();
    await using var conn = new MySqlConnection(BuildConnString());
    await conn.OpenAsync();
    var sql = @"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA=@schema";
    await using var cmd = new MySqlCommand(sql, conn);
    cmd.Parameters.AddWithValue("@schema", connectionName);
    await using var reader = await cmd.ExecuteReaderAsync();
    while (await reader.ReadAsync())
    {
        var table = reader.GetString(0);
        list.Add(new DBTableDto { Name = table });
    }
    return list;
});

app.MapGet("/fields", async (string connectionName, string tableName) =>
{
    var list = new List<DBFieldDto>();
    await using var conn = new MySqlConnection(BuildConnString());
    await conn.OpenAsync();
    var sql = @"SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS
                WHERE TABLE_SCHEMA=@schema AND TABLE_NAME=@table ORDER BY ORDINAL_POSITION";
    await using var cmd = new MySqlCommand(sql, conn);
    cmd.Parameters.AddWithValue("@schema", connectionName);
    cmd.Parameters.AddWithValue("@table", tableName);
    await using var reader = await cmd.ExecuteReaderAsync();
    while (await reader.ReadAsync())
    {
        list.Add(new DBFieldDto { Name = reader.GetString(0), Type = reader.GetString(1) });
    }
    return list;
});

app.Run();
