using CCDto.entity.Base;
using CCDto.entity.Dto.Request;
using CCDto.entity.Dto.Response;
using FreeSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CCDto.BaseService.Crud
{
    public class AsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TUpdateInput> :
        //BaseRepository<TEntity, TPrimaryKey>,
        IAsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TUpdateInput>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        public static IFreeSql freeSql;

        IBaseRepository<TEntity, TPrimaryKey> _dbRepository { get; set; }

        public AsyncCrudAppService(IFreeSql fsql)
        {
            freeSql = fsql;
            _dbRepository = freeSql.GetRepository<TEntity, TPrimaryKey>();
        }

        public int ExecuteNonQuery(string sql)
        {
            return _dbRepository.Orm.Ado.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// GetAsync
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<TEntity> GetAsync(TPrimaryKey Id)
        {
            long entityid = 0;
            if (typeof(TPrimaryKey) == typeof(int))
            {
                entityid = Convert.ToInt32(Id);
            }

            if (typeof(TPrimaryKey) == typeof(long))
            {
                entityid = Convert.ToInt64(Id);
            }
            if (entityid > 0)
            {
                return await _dbRepository.GetAsync(Id);
            }
            return null;
        }

        public virtual async Task<bool> SaveAsync(TEntityDto dto)
        {
            var entity = default(TEntity);
            if (entity.IsAboveZero())
            {
                var result = await _dbRepository.UpdateAsync(entity);
                return true;
            }
            else
            {
                var result = await _dbRepository.InsertAsync(entity);
                return result.IsAboveZero();
            }
        }

        #region 删除变逻辑删除
        public async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var entitys = await GetAllAsync(predicate);
            foreach (var entity in entitys)
            {
                entity.IsDelete = true;
            }
            return await _dbRepository.UpdateAsync(entitys, cancellationToken);
        }
        public async Task<int> DeleteAsync(IEnumerable<TEntity> entitys, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entitys)
            {
                entity.IsDelete = true;
            }
            return await _dbRepository.UpdateAsync(entitys);
        }
        public async Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            entity.IsDelete = true;
            return await _dbRepository.UpdateAsync(entity, cancellationToken);
        }
        public async Task<int> DeleteAsync(TPrimaryKey id, CancellationToken cancellationToken = default)
        {
            var entity = await GetAsync(id);
            entity.IsDelete = true;
            return await _dbRepository.UpdateAsync(entity, cancellationToken);
        }
        #endregion

        public virtual async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> exp = null)
        {
            return await _dbRepository.WhereIf(exp != null, exp).Where(o => !o.IsDelete).ToListAsync();
        }

        public virtual async Task<PagedResultDto<TEntityDto>> GetPagingAsync(Expression<Func<TEntity, bool>> exp, TGetAllInput input)
        {
            var query = _dbRepository
                 .WhereIf(exp != null, exp)
                 .Where(o => !o.IsDelete)
                 .Count(out var total); //总记录数量
            if (input != null)
            {
                query = ApplySorting(query, input);
                query = ApplyPaging(query, input);
            }
            var list = await query.ToListAsync();
            return new PagedResultDto<TEntityDto>(
                total,
                input as PagedResultRequestDto,
                list.Select(MapToEntityDto).ToList()
            );
        }

        /// <summary>
        /// Maps <typeparamref name="TEntity"/> to <typeparamref name="TEntityDto"/>.
        /// It uses <see cref="IObjectMapper"/> by default.
        /// It can be overrided for custom mapping.
        /// </summary>
        public virtual TEntityDto MapToEntityDto(TEntity entity)
        {
            //return entity.MapTo<TEntityDto>();
            return default(TEntityDto);
        }

        /// <summary>
        /// Should apply sorting if needed.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="input">The input.</param>
        protected virtual ISelect<TEntity> ApplySorting(ISelect<TEntity> query, TGetAllInput input)
        {
            //Try to sort query if available
            var sortInput = input as ISortedResultRequest;
            if (sortInput != null)
            {
                if (!string.IsNullOrWhiteSpace(sortInput.Sorting))
                {
                    return query.OrderBy(sortInput.Sorting);
                }
            }

            //IQueryable.Task requires sorting, so we should sort if Take will be used.
            if (input is ILimitedResultRequest)
            {
                return query.OrderByDescending(e => e.Id);
            }

            //No sorting
            return query;
        }

        /// <summary>
        /// Should apply paging if needed.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="input">The input.</param>
        protected virtual ISelect<TEntity> ApplyPaging(ISelect<TEntity> query, TGetAllInput input)
        {
            //Try to use paging if available
            var pagedInput = input as IPagedResultRequest;
            if (pagedInput != null)
            {
                return pagedInput.PageSize <= 0 ? query : query.Page(pagedInput.PageIndex, pagedInput.PageSize);
            }

            //Try to limit query result if available
            var limitedInput = input as ILimitedResultRequest;
            if (limitedInput != null)
            {
                return pagedInput.PageSize <= 0 ? query : query.Take(limitedInput.PageSize);
            }

            //No paging
            return query;
        }


    }
}
