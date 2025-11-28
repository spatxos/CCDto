using System.Collections.Generic;
using System.Threading.Tasks;
using DispatchProxyDemo.Proxy.DTOs;

namespace DispatchProxyDemo.Proxy.Interfaces;

[DispatchProxyDemo.Proxy.ServiceName("dbconnection")]
public interface IDBConnectionService
{
    [DispatchProxyDemo.Proxy.Route("connections")]
    Task<List<DBConnectionDto>> GetConnections();

    [DispatchProxyDemo.Proxy.Route("tables")]
    Task<List<DBTableDto>> GetTables(string connectionName);

    [DispatchProxyDemo.Proxy.Route("fields")]
    Task<List<DBFieldDto>> GetFields(string connectionName, string tableName);
}
