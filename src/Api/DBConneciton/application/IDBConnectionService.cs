using api.dbconnecion.entity;
using api.dbconnecion.entity.Dto;
using CCDto.BaseService.Crud;

namespace CCDto.application
{
    public interface IDBConnectionService : IAsyncCrudAppService<DBConnection, DBConnectionDto, int, DBConnectionsPagedResultRequestDto, DBConnectionDto>
    {
    }
    //public interface IDBConnectionService
    //{
    //    /// <summary>
    //    /// ��ȡ
    //    /// </summary>
    //    /// <returns></returns>
    //    [HttpGet]
    //    DBConnection GetDbEntity(int Id);
    //}
}
