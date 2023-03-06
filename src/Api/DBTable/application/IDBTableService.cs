using api.dbtable.application.Dto;
using api.dbtable.entity;
using CCDto.application.Service.Crud;

namespace api.dbtable.application
{
    public interface IDBTableService : IAsyncCrudAppService<DBTable, DBTableDto, int, DBTablesPagedResultRequestDto, DBTableDto>
    {
    }
}
