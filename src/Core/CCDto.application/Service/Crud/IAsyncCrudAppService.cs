using CCDto.application.Service.Crud.Dto;
using CCDto.application.Service.Crud.Dto.Request;
using CCDto.application.Service.Crud.Dto.Response;
using CCDto.entity.Base;
using CCDto.entity.DtoColumn;
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

        List<DtoColumn> GetDtoColumns(DtoColumnRequest dtoColumnRequest, Type type = null);

        TEntityDto MapToEntityDto(TEntity entity);
    }
}
