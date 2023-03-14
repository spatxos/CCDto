using CCDto.entity.Dto.Request;
using CCDto.entity.Dto.Response;
using CCDto.entity.DtoColumn;
using CCDto.entity.DtoColumn.Db;
using FreeSql;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CCDto.application.Service.Crud
{
    //public interface IAsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TUpdateInput> : IApplicationService, IBaseRepository<TEntity, TPrimaryKey> where TEntity : class
    public interface IAsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TUpdateInput> : IApplicationService where TEntity : class
    {
        Task<TEntity> GetAsync(TPrimaryKey Id);
        int ExecuteNonQuery(string sql);
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> exp = null);

        Task<bool> SaveAsync(TEntityDto dto);

        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<int> DeleteAsync(IEnumerable<TEntity> entitys, CancellationToken cancellationToken = default);
        Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<int> DeleteAsync(TPrimaryKey id, CancellationToken cancellationToken = default);
        Task<PagedResultDto<TEntityDto>> GetPagingAsync(Expression<Func<TEntity, bool>> exp, TGetAllInput input);

        List<DtoColumn> GetDtoColumns(DtoColumnRequest dtoColumnRequest, Type type = null, List<CustomOption> customOptions = null);

        TEntityDto MapToEntityDto(TEntity entity);
    }
}
