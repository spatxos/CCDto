using CCDto.application.Service.Crud;
using CCDto.application.Service.DBConnections.Dto;
using CCDto.common.Encrypt;
using CCDto.entity;
using CCDto.entity.Base;
using CCDto.entity.Request;

using System.Linq;
using CCDto.entity.Table;

namespace CCDto.application.Service.DBConnections
{
    public class DBConnectionService : AsyncCrudAppService<DBConnection, DBConnectionDto, int,DBConnectionsPagedResultRequestDto,DBConnectionDto>, IDBConnectionService
    {
        public ReturnMsg returnMsg = new ReturnMsg();
        public DBConnectionService(IFreeSql fsql) : base(fsql)
        {

        }
    }
}
