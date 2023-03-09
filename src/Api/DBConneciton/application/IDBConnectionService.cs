using dbconnecion.application.Dto;
using api.dbconnecion.entity;
using CCDto.application.Service.Crud;
using Panda.DynamicWebApi;
using Panda.DynamicWebApi.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace dbconnecion.application
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
