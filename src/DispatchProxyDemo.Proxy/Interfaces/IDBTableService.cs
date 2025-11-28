using System.Collections.Generic;
using System.Threading.Tasks;
using DispatchProxyDemo.Proxy.DTOs;

namespace DispatchProxyDemo.Proxy.Interfaces;

[DispatchProxyDemo.Proxy.ServiceName("dbconnection")]
public interface IDBTableService
{
    [DispatchProxyDemo.Proxy.Route("tables")]
    Task<List<DBTableDto>> GetTables(string connectionName);
}
