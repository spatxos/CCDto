using com.msung.application.Service.Crud;
using com.msung.application.Service.TableNames.Dto;
using com.msung.common.Encrypt;
using com.msung.entity;
using com.msung.entity.Base;
using com.msung.entity.Request;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using com.msung.entity.Table;
using com.msung.entity.Table.TableSpace;

namespace com.msung.application.Service.TableNames
{
    public class TableNameService : AsyncCrudAppService<TableName, TableNameDto, int,TableNamesPagedResultRequestDto,TableNameDto>, ITableNameService
    {
        public ReturnMsg returnMsg = new ReturnMsg();
        public TableNameService(IFreeSql fsql) : base(fsql)
        {

        }
    }
}
