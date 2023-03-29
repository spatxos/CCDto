using CCDto.application.Service.Crud;
using CCDto.entity.Base;
using CCDto.entity.DtoColumn;
using System.Collections.Generic;
using System;
using CCDto.entity.DtoColumn.Db;
using api.dbtable.entity;
using CCDto.entity.Dto.Request;
using api.dbtable.entity.Dto;
using CCDto.application;

namespace api.dbtable.application
{
    public class DBTableService : AsyncCrudAppService<DBTable, DBTableDto, int, DBTablesPagedResultRequestDto, DBTableDto>, IDBTableService
    {
        public ReturnMsg returnMsg = new ReturnMsg();
        //public IDBFieldService _dBFieldService;

        public DBTableService(IFreeSql fsql) : base(fsql)
        {
            //_dBConnectionService = new DBConnectionService(fsql);
            //_dBFieldService = new DBFieldService(fsql);
        }
        public override List<DtoColumn> GetDtoColumns(DtoColumnRequest dtoColumnRequest, Type type = null, List<CustomOption> cos = null)
        {
            var customOptions = new List<CustomOption>();
            customOptions.Add(new CustomOption()
            {
                PropertyName = "DBConnectionId",
                Func = (dtoColumn) =>
                {
                    //attr.OptionSql += dtoColumnRequest.QueryId;

                    //var options = _dBConnectionService.ChangeRepository().Where(o => !o.IsDelete).Select(o => new Option() { Id = o.Id, Value = o.DBConnectionName }).ToList();
                    ////var options = freeSql.Select<Option>().WithSql(@"select Id,DBConnectionName value from TBL_DBCONNECTION where isdelete=0 ").ToList();

                    //if (options.Any())
                    //{
                    //    if (!options.Any(o => o.Id == 0))
                    //    {
                    //        dtoColumn.Options.Add((0, "��ѡ��"));
                    //    }
                    //    dtoColumn.Options.AddRange(options.Select(o => ((object)o.Id, (object)o.Value)));
                    //}
                    return dtoColumn;
                }
            });
            customOptions.Add(new CustomOption()
            {
                PropertyName = "DBFieldId",
                Func = (dtoColumn) =>
                {
                    //var conns = _dBConnectionService.ChangeRepository().Where(o => !o.IsDelete).ToList();
                    //var fields = _dBFieldService.ChangeRepository().Where(o => !o.IsDelete).ToList();
                    //var tables = this.Where(o => !o.IsDelete).ToList();
                    //var cascaders = new List<DbCascader>();
                    //foreach (var conn in conns)
                    //{
                    //    foreach (var table in tables.Where(o => o.DBConnectionId == conn.Id))
                    //    {
                    //        foreach (var field in fields.Where(o => o.DBTableId == table.Id))
                    //        {
                    //            cascaders.Add(new DbCascader()
                    //            {
                    //                id1 = conn.Id,
                    //                id2 = table.Id,
                    //                id3 = field.Id,
                    //                name1 = conn.DBConnectionName,
                    //                name2 = table.DBTableName,
                    //                name3 = field.DBFieldName
                    //            });
                    //        }
                    //    }
                    //}
                    ////cascaders = freeSql.Ado.Query<DbCascader>(@"select t1.id id1,t1.DBConnectionName name1,t2.id id2,t2.DBTableName name2,t3.Id id3,t3.DBFieldName name3  from TBL_DBCONNECTION t1 left join TBL_DBTABLE t2 on t1.id = t2.DBConnectionId left join TBL_DBFIELD t3 on t2.id = t3.DBTableId WHERE t1.IsDelete = 0 and t2.IsDelete = 0 and t3.IsDelete = 0 ").ToList();
                    //dtoColumn.CascaderOptions.AddRange(OptionHelper.DealwithCascader(cascaders));
                    return dtoColumn;
                }
            });
            return base.GetDtoColumns(dtoColumnRequest, type, customOptions);
        }
    }
}
