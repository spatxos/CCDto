using CCDto.application.Service.Crud;
using CCDto.application.Service.DBTables.Dto;
using CCDto.common.Encrypt;
using CCDto.entity;
using CCDto.entity.Base;
using CCDto.entity.Request;

using System.Linq;
using CCDto.entity.Table;
using CCDto.application.Service.Crud.Dto.Request;
using CCDto.entity.DtoColumn;
using System.Collections.Generic;
using System;
using CCDto.entity.DtoColumn.Db;
using CCDto.entity.DtoColumn.Option;
using CCDto.common.Option;

namespace CCDto.application.Service.DBTables
{
    public class DBTableService : AsyncCrudAppService<DBTable, DBTableDto, int, DBTablesPagedResultRequestDto, DBTableDto>, IDBTableService
    {
        public ReturnMsg returnMsg = new ReturnMsg();
        public DBTableService(IFreeSql fsql) : base(fsql)
        {

        }
        public override List<DtoColumn> GetDtoColumns(DtoColumnRequest dtoColumnRequest, Type type = null, List<CustomOption> cos = null)
        {
            var customOptions = new List<CustomOption>();
            customOptions.Add(new CustomOption()
            {
                PropertyName = "DBConnectionId",
                Func = (dtoColumn) => {
                    //attr.OptionSql += dtoColumnRequest.QueryId;
                    var options = freeSql.Select<Option>().WithSql(@"select Id,DBConnectionName value from TBL_DBCONNECTION where isdelete=0 ").ToList();

                    if (options.Any())
                    {
                        if (!options.Any(o => o.Id == 0))
                        {
                            dtoColumn.Options.Add((0, "²»Ñ¡Ôñ"));
                        }
                        dtoColumn.Options.AddRange(options.Select(o => ((object)o.Id, (object)o.Value)));
                    }
                    return dtoColumn;
                }
            });
            customOptions.Add(new CustomOption()
            {
                PropertyName = "DBFieldId",
                Func = (dtoColumn) => {
                    var cascaders = freeSql.Ado.Query<DbCascader>(@"select t1.id id1,t1.DBConnectionName name1,t2.id id2,t2.DBTableName name2,t3.Id id3,t3.DBFieldName name3  from TBL_DBCONNECTION t1 left join TBL_DBTABLE t2 on t1.id = t2.DBConnectionId left join TBL_DBFIELD t3 on t2.id = t3.DBTableId WHERE t1.IsDelete = 0 and t2.IsDelete = 0 and t3.IsDelete = 0 ").ToList();
                    dtoColumn.CascaderOptions.AddRange(OptionHelper.DealwithCascader(cascaders));
                    return dtoColumn;
                }
            });
            return base.GetDtoColumns(dtoColumnRequest, type, customOptions);
        }
    }
}
