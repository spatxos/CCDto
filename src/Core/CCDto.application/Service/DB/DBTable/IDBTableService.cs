using CCDto.application.Service.Crud;
using CCDto.application.Service.DBTables.Dto;
using CCDto.entity;
using CCDto.entity.Base;
using CCDto.entity.Request;
using CCDto.entity.Table;

namespace CCDto.application.Service.DBTables
{
    public interface IDBTableService : IAsyncCrudAppService<DBTable, DBTableDto, int, DBTablesPagedResultRequestDto, DBTableDto>
    {
    }
}
