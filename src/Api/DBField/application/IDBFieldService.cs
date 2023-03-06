using api.dbfield.application.Dto;
using api.dbfield.entity;
using CCDto.application.Service.Crud;

namespace api.dbfield.application
{
    public interface IDBFieldService : IAsyncCrudAppService<DBField, DBFieldDto, int, DBFieldsPagedResultRequestDto, DBFieldDto>
    {
    }
}
