using api.dbconnecion.application.Dto;
using api.dbconnecion.entity;
using CCDto.application.Service.Crud;
using CCDto.entity.Base;

namespace api.dbconnecion.application
{
    public class DBConnectionService : AsyncCrudAppService<DBConnection, DBConnectionDto, int, DBConnectionsPagedResultRequestDto, DBConnectionDto>, IDBConnectionService
    {
        public ReturnMsg returnMsg = new ReturnMsg();
        public DBConnectionService(IFreeSql fsql) : base(fsql)
        {

        }
    }
}
