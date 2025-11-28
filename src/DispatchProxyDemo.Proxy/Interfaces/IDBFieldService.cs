using System.Collections.Generic;
using System.Threading.Tasks;
using DispatchProxyDemo.Proxy.DTOs;

namespace DispatchProxyDemo.Proxy.Interfaces;

[DispatchProxyDemo.Proxy.ServiceName("dbconnection")]
public interface IDBFieldService
{
    [DispatchProxyDemo.Proxy.Route("fields")]
    Task<List<DBFieldDto>> GetFields(string connectionName, string tableName);
}
