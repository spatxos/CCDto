using CCDto.application.Service.Crud;
using CCDto.application.Service.DBFields.Dto;
using CCDto.common.Encrypt;
using CCDto.entity;
using CCDto.entity.Base;
using CCDto.entity.Request;

using System.Linq;
using CCDto.entity.Table;

namespace CCDto.application.Service.DBFields
{
    public class DBFieldService : AsyncCrudAppService<DBField, DBFieldDto, int,DBFieldsPagedResultRequestDto,DBFieldDto>, IDBFieldService
    {
        public ReturnMsg returnMsg = new ReturnMsg();
        public DBFieldService(IFreeSql fsql) : base(fsql)
        {

        }
    }
}
