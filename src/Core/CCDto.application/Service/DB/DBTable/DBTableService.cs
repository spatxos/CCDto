using CCDto.application.Service.Crud;
using CCDto.application.Service.DBTables.Dto;
using CCDto.common.Encrypt;
using CCDto.entity;
using CCDto.entity.Base;
using CCDto.entity.Request;

using System.Linq;
using CCDto.entity.Table;

namespace CCDto.application.Service.DBTables
{
    public class DBTableService : AsyncCrudAppService<DBTable, DBTableDto, int,DBTablesPagedResultRequestDto,DBTableDto>, IDBTableService
    {
        public ReturnMsg returnMsg = new ReturnMsg();
        public DBTableService(IFreeSql fsql) : base(fsql)
        {

        }
    }
}
