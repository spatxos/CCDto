using api.dbfield.entity;
using api.dbfield.entity.Dto;
using CCDto.application.Service.Crud;

namespace CCDto.application
{
    public interface IDBFieldService : IAsyncCrudAppService<DBField, DBFieldDto, int, DBFieldsPagedResultRequestDto, DBFieldDto>
    {
    }
}
