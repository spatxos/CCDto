using api.dbconnecion.entity;
using api.dbconnecion.entity.Dto;
using CCDto.Service.Crud;

namespace api.dbconnecion.application.Services
{
    public interface IDBConnectionService : IAsyncCrudAppService<DBConnection, DBConnectionDto, int, DBConnectionsPagedResultRequestDto, DBConnectionDto>
    {
    }
    //public interface IDBConnectionService
    //{
    //    /// <summary>
    //    /// ªÒ»°
    //    /// </summary>
    //    /// <returns></returns>
    //    [HttpGet]
    //    DBConnection GetDbEntity(int Id);
    //}
}
