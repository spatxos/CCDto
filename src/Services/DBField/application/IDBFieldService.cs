using api.dbfield.entity;
using api.dbfield.entity.Dto;
using CCDto.Service.Crud;

namespace CCDto.application
{
    public interface IDBFieldService : IAsyncCrudAppService<DBField, DBFieldDto, int, DBFieldsPagedResultRequestDto, DBFieldDto>
    {
    }
}
