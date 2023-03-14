using api.dbtable.entity;
using api.dbtable.entity.Dto;
using CCDto.application.Service.Crud;

namespace CCDto.application
{
    public interface IDBTableService : IAsyncCrudAppService<DBTable, DBTableDto, int, DBTablesPagedResultRequestDto, DBTableDto>
    {
    }
}
