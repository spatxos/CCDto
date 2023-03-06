using api.dbconnecion.application.Dto;
using api.dbconnecion.entity;
using CCDto.application.Service.Crud;

namespace api.dbconnecion.application
{
    public interface IDBConnectionService : IAsyncCrudAppService<DBConnection, DBConnectionDto, int, DBConnectionsPagedResultRequestDto, DBConnectionDto>
    {
    }
}
