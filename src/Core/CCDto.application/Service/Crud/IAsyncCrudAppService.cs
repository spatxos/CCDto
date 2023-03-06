using CCDto.entity.Dto.Request;
using CCDto.entity.Dto.Response;
using CCDto.entity.DtoColumn;
using CCDto.entity.DtoColumn.Db;
using FreeSql;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CCDto.application.Service.Crud
{
    public interface IAsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TUpdateInput> : IApplicationService, IBaseRepository<TEntity, TPrimaryKey> where TEntity : class
    {
        int ExecuteNonQuery(string sql);
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> exp = null);

        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> exp = null);

        bool Save(TEntityDto dto);

        Task<bool> SaveAsync(TEntityDto dto);

        PagedResultDto<TEntityDto> GetPaging(Expression<Func<TEntity, bool>> exp, TGetAllInput input);

        Task<PagedResultDto<TEntityDto>> GetPagingAsync(Expression<Func<TEntity, bool>> exp, TGetAllInput input);

        List<DtoColumn> GetDtoColumns(DtoColumnRequest dtoColumnRequest, Type type = null, List<CustomOption> customOptions = null);

        TEntityDto MapToEntityDto(TEntity entity);
    }
}
