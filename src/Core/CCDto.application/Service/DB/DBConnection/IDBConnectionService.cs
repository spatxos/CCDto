using CCDto.application.Service.Crud;
using CCDto.application.Service.DBConnections.Dto;
using CCDto.entity;
using CCDto.entity.Base;
using CCDto.entity.Request;
using CCDto.entity.Table;

namespace CCDto.application.Service.DBConnections
{
    public interface IDBConnectionService : IAsyncCrudAppService<DBConnection, DBConnectionDto, int, DBConnectionsPagedResultRequestDto, DBConnectionDto>
    {
    }
}
