using CCDto.entity.Base;
using api.dbfield.entity;
using api.dbfield.entity.Dto;
using CCDto.application;
using CCDto.BaseService.Crud;

namespace api.dbfield.application
{
    public class DBFieldService : AsyncCrudAppService<DBField, DBFieldDto, int,DBFieldsPagedResultRequestDto,DBFieldDto>, IDBFieldService
    {
        public ReturnMsg returnMsg = new ReturnMsg();
        //public IDBTableService _dBTableService;

        public DBFieldService(IFreeSql fsql) : base(fsql)
        {
            //_dBTableService = new DBTableService(fsql);
            //this.ChangeRepository();
        }

        //public override List<DtoColumn> GetDtoColumns(DtoColumnRequest dtoColumnRequest, Type type = null, List<CustomOption> cos = null)
        //{
        //    var customOptions = new List<CustomOption>();
        //    customOptions.Add(new CustomOption()
        //    {
        //        PropertyName = "DBTableId",
        //        Func = (dtoColumn) => {
        //            //attr.OptionSql += dtoColumnRequest.QueryId;
        //            //var options = _dBTableService.ChangeRepository().Where(o => !o.IsDelete).Select(o => new Option() { Id = o.Id, Value = o.DBTableName }).ToList();
        //            ////var options = freeSql.Select<Option>().WithSql(@"select Id,DBTableName value from TBL_DBTABLE where isdelete=0 ").ToList();

        //            //if (options.Any())
        //            //{
        //            //    if (!options.Any(o => o.Id == 0))
        //            //    {
        //            //        dtoColumn.Options.Add((0, "��ѡ��"));
        //            //    }
        //            //    dtoColumn.Options.AddRange(options.Select(o => ((object)o.Id, (object)o.Value)));
        //            //}
        //            return dtoColumn;
        //        }
        //    });
        //    return base.GetDtoColumns(dtoColumnRequest, type, customOptions);
        //}

    }
}
