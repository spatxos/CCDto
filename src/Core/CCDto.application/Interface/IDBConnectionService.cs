using api.dbconnecion.entity;
using CCDto.application.Service.Crud;
using api.dbconnecion.entity.Dto;

namespace CCDto.application
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
