using com.msung.application.Service.Crud;
using com.msung.application.Service.TableNames.Dto;
using com.msung.entity;
using com.msung.entity.Base;
using com.msung.entity.Request;
using com.msung.entity.Table;
using com.msung.entity.Table.TableSpace;

namespace com.msung.application.Service.TableNames
{
    public interface ITableNameService : IAsyncCrudAppService<TableName, TableNameDto, int, TableNamesPagedResultRequestDto, TableNameDto>
    {
    }
}
