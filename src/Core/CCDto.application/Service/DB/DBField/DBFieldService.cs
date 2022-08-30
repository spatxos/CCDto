using CCDto.application.Service.Crud;
using CCDto.application.Service.DBFields.Dto;
using CCDto.common.Encrypt;
using CCDto.entity;
using CCDto.entity.Base;
using CCDto.entity.Request;

using System.Linq;
using CCDto.entity.Table;
using CCDto.application.Service.Crud.Dto.Request;
using CCDto.entity.DtoColumn.Db;
using CCDto.entity.DtoColumn;
using System.Collections.Generic;
using CCDto.entity.DtoColumn.Option;
using System;

namespace CCDto.application.Service.DBFields
{
    public class DBFieldService : AsyncCrudAppService<DBField, DBFieldDto, int,DBFieldsPagedResultRequestDto,DBFieldDto>, IDBFieldService
    {
        public ReturnMsg returnMsg = new ReturnMsg();
        public DBFieldService(IFreeSql fsql) : base(fsql)
        {

        }

        public override List<DtoColumn> GetDtoColumns(DtoColumnRequest dtoColumnRequest, Type type = null, List<CustomOption> cos = null)
        {
            var customOptions = new List<CustomOption>();
            customOptions.Add(new CustomOption()
            {
                PropertyName = "DBTableId",
                Func = (dtoColumn) => {
                    //attr.OptionSql += dtoColumnRequest.QueryId;
                    var options = freeSql.Select<Option>().WithSql(@"select Id,DBTableName value from TBL_DBTABLE where isdelete=0 ").ToList();

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

            return base.GetDtoColumns(dtoColumnRequest, type, customOptions);
        }

    }
}
