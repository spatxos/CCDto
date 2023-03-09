using dbconnecion.application.Dto;
using api.dbconnecion.entity;
using CCDto.application.Service.Crud;
using CCDto.entity.Base;
using Microsoft.AspNetCore.Mvc;
using Panda.DynamicWebApi;
using Panda.DynamicWebApi.Attributes;
using MathNet.Numerics.Statistics.Mcmc;
using System;

namespace dbconnecion.application
{
    [DynamicWebApi]
    [Service("DBConnection")]
    public class DBConnectionAppService : AsyncCrudAppService<DBConnection, DBConnectionDto, int, DBConnectionsPagedResultRequestDto, DBConnectionDto>, IDBConnectionService
    {
        public ReturnMsg returnMsg = new ReturnMsg();
        public DBConnectionAppService(IFreeSql fsql) : base(fsql)
        {
        }
    }
    //[Service("DBConnection")]
    ///// <summary>
    ///// 数据库服务
    ///// </summary>
    //public class DBConnectionAppService : IDBConnectionService
    //{
    //    IAsyncCrudAppService<DBConnection, DBConnectionDto, int, DBConnectionsPagedResultRequestDto, DBConnectionDto> _asyncCrudAppService;
    //    public ReturnMsg returnMsg = new ReturnMsg();
    //    public DBConnectionAppService(IFreeSql fsql)
    //    {
    //        _asyncCrudAppService = new AsyncCrudAppService<DBConnection, DBConnectionDto, int, DBConnectionsPagedResultRequestDto, DBConnectionDto>(fsql);
    //    }

    //    /// <summary>
    //    /// 获取
    //    /// </summary>
    //    /// <returns></returns>
    //    [HttpGet]
    //    public DBConnection GetDbEntity(int Id)
    //    {
    //        return _asyncCrudAppService.Get(Id);
    //    }
    //}
}
