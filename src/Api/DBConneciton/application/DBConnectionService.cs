using api.dbconnecion.entity;
using CCDto.entity.Base;
using api.dbconnecion.entity.Dto;
using CCDto.application;
using Panda.DynamicWebApi.Attributes;
using CCDto.BaseService.Crud;

namespace api.dbconnecion.application
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
    //    //public DBConnectionAppService(IFreeSql fsql)
    //    //{
    //    //    _asyncCrudAppService = new AsyncCrudAppService<DBConnection, DBConnectionDto, int, DBConnectionsPagedResultRequestDto, DBConnectionDto>(fsql);
    //    //}
    //    public DBConnectionAppService(IFreeSql fsql, IAsyncCrudAppService<DBConnection, DBConnectionDto, int, DBConnectionsPagedResultRequestDto, DBConnectionDto> asyncCrudAppService)
    //    {
    //        _asyncCrudAppService = asyncCrudAppService;
    //        //_asyncCrudAppService = new AsyncCrudAppService<DBConnection, DBConnectionDto, int, DBConnectionsPagedResultRequestDto, DBConnectionDto>(fsql);
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
