using CCDto.application.Service.Crud;
using CCDto.application.Service.DBFields.Dto;
using CCDto.entity;
using CCDto.entity.Base;
using CCDto.entity.Request;
using CCDto.entity.Table;

namespace CCDto.application.Service.DBFields
{
    public interface IDBFieldService : IAsyncCrudAppService<DBField, DBFieldDto, int, DBFieldsPagedResultRequestDto, DBFieldDto>
    {
    }
}
