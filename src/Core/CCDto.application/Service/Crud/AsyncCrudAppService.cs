using AutoMapper;
using CCDto.application.Service.Crud.Dto;
using CCDto.application.Service.Crud.Dto.Request;
using CCDto.application.Service.Crud.Dto.Response;
using CCDto.common;
using CCDto.common.AutoMapper;
using CCDto.common.FreeSql;
using CCDto.entity.Base;
using CCDto.entity.DtoColumn;
using CCDto.entity.DtoColumn.Db;
using CCDto.entity.DtoColumn.Option;
using CCDto.entity.FreeSql;
using FreeSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CCDto.application.Service.Crud
{
    public class AsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TUpdateInput> :
        BaseRepository<TEntity, TPrimaryKey>,
        IAsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TUpdateInput>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected static IBaseRepository<TEntity> _dbRepository;
        public static IFreeSql freeSql;

        public AsyncCrudAppService(IFreeSql fsql) : base(fsql, null, null)
        {
            var dbKey = GetBbKey(typeof(TEntity));
            if (string.IsNullOrWhiteSpace(dbKey))
            {
                freeSql = fsql;
                _dbRepository = freeSql.GetRepository<TEntity, TPrimaryKey>();
            }
            else
            {
                freeSql = fsql.Change(GetBbKey(typeof(TEntity)));
                _dbRepository = freeSql.GetRepository<TEntity, TPrimaryKey>();
            }
        }
        public string GetBbKey(Type type = null)
        {
            var dbKey = default(string);
            if (type != null)
            {
                var attribute = (MultiDBAttribute)Attribute.GetCustomAttribute(type, typeof(MultiDBAttribute));
                if (attribute != null)
                {
                    dbKey = attribute.DbName;
                }
            }
            return dbKey;
        }
        public int ExecuteNonQuery(string sql)
        {
            return _dbRepository.Orm.Ado.ExecuteNonQuery(sql);
        }
        public new TEntity Get(TPrimaryKey Id)
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
                return base.Get(Id);
            }
            return null;
        }

        public virtual bool Save(TEntityDto dto)
        {
            var entity = dto.MapTo<TEntity>();
            if (entity.IsAboveZero())
            {
                _dbRepository.Update(entity);
                return true;
            }
            else
            {
                var newentity = _dbRepository.Insert(entity);
                return newentity.IsAboveZero();
            }
        }

        public virtual async Task<bool> SaveAsync(TEntityDto dto)
        {
            var entity = dto.MapTo<TEntity>();
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

        public override int Delete(IEnumerable<TEntity> entitys)
        {
            foreach (var entity in entitys)
            {
                entity.IsDelete = true;
            }
            return base.Update(entitys);
        }

        public override int Delete(TEntity entity)
        {
            entity.IsDelete = true;
            return base.Update(entity);
        }
        public override int Delete(TPrimaryKey id)
        {
            var entity = Get(id);
            entity.IsDelete = true;
            return base.Update(entity);
        }
        public override int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var entitys = GetAll(predicate);
            foreach (var entity in entitys)
            {
                entity.IsDelete = true;
            }
            return base.Update(entitys);
        }

        //public override Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        //{
        //    return base.DeleteAsync(predicate, cancellationToken);
        //}


        public override async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var entitys = await GetAllAsync(predicate);
            foreach (var entity in entitys)
            {
                entity.IsDelete = true;
            }
            return await base.UpdateAsync(entitys, cancellationToken);
        }
        public override async Task<int> DeleteAsync(IEnumerable<TEntity> entitys, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entitys)
            {
                entity.IsDelete = true;
            }
            return await base.UpdateAsync(entitys);
        }
        public override async Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            entity.IsDelete = true;
            return await base.UpdateAsync(entity, cancellationToken); 
        }
        public override async Task<int> DeleteAsync(TPrimaryKey id, CancellationToken cancellationToken = default)
        {
            var entity = await GetAsync(id);
            entity.IsDelete = true;
            return await base.UpdateAsync(entity, cancellationToken);
        }
        #endregion

        public virtual List<TEntity> GetAll(Expression<Func<TEntity, bool>> exp = null)
        {
            return _dbRepository.WhereIf(exp != null, exp).Where(o => !o.IsDelete).ToList();
        }

        public virtual async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> exp = null)
        {
            return await _dbRepository.WhereIf(exp != null, exp).Where(o => !o.IsDelete).ToListAsync();
        }

        public virtual PagedResultDto<TEntityDto> GetPaging(Expression<Func<TEntity, bool>> exp, TGetAllInput input)
        {
            var query = _dbRepository
                 .WhereIf(exp != null, exp)
                 .Where(o=>!o.IsDelete)
                 .Count(out var total); //总记录数量

            if (input != null)
            {
                query = ApplySorting(query, input);
                query = ApplyPaging(query, input);
            }
            var list = query.ToList();

            return new PagedResultDto<TEntityDto>(
                total,
                input as PagedResultRequestDto,
                list.Select(MapToEntityDto).ToList()
            );
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
            return entity.MapTo<TEntityDto>();
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

        public virtual List<DtoColumn> GetDtoColumns(DtoColumnRequest dtoColumnRequest,Type type = null,List<CustomOption> customOptions = null)
        {
            var showColumns = new List<DtoColumn>();
            if (type == null)
            {
                type = typeof(TEntityDto);
            }
            //var t = typeof(TEntityDto);
            var properties = type.GetProperties();
            var sort = 0;
            foreach (var property in properties)
            {
                var attr = (DtoColumnAttribute)Attribute.GetCustomAttribute(property, typeof(DtoColumnAttribute));
                if (attr != null)
                {
                    if (dtoColumnRequest.IsCheckDisabled)
                    {
                        if (!attr.IsDisabled)
                        {
                            showColumns.Add(new DtoColumn()
                            {
                                PropertyName = property.Name,
                                ShowName = attr.ShowName,
                                SortId = attr.SortId > 0 ? attr.SortId : sort
                            });
                            sort++;
                        }
                    }
                    else if (dtoColumnRequest.IsCheckEdit)
                    {
                        if (attr.IsEdit)
                        {
                            var dtoColumn = new DtoColumn()
                            {
                                PropertyName = property.Name,
                                ShowName = attr.ShowName,
                                SortId = attr.SortId > 0 ? attr.SortId : sort,
                                ControlHtml = GetControlHtml(attr).ToString(),
                                EditType = attr.EditType.ToString(),
                            };
                            if (attr.EditType == EditType.select)
                            {
                                var customOption = customOptions.FirstOrDefault(o => o.PropertyName == property.Name);
                                if (customOption != null)
                                {
                                    dtoColumn = customOption.Func(dtoColumn);
                                }
                            }
                            if (attr.EditType == EditType.boolselect)
                            {
                                dtoColumn.EditType = EditType.select.ToString();
                                dtoColumn.Options.Add((false, "否"));
                                dtoColumn.Options.Add((true, "是"));
                            }
                            if (attr.EditType == EditType.statusselect)
                            {
                                dtoColumn.EditType = EditType.select.ToString();
                                dtoColumn.Options.Add((false, "禁用"));
                                dtoColumn.Options.Add((true, "启用"));
                            }
                            if (attr.EditType == EditType.enumselect)
                            {
                                dtoColumn.EditType = EditType.select.ToString();
                                var fields = attr.Type.GetFields();
                                if (!fields.Where(o=> !o.IsSpecialName).Any(o => (int)o.GetRawConstantValue() == 0))
                                {
                                    dtoColumn.Options.Add((0, "不选择"));
                                }
                                foreach (var field in fields)
                                {
                                    if (!field.IsSpecialName)
                                    {
                                        dtoColumn.Options.Add((field.GetRawConstantValue(), field.Name));
                                    }
                                }
                            }

                            if (attr.EditType == EditType.cascader)
                            {
                                dtoColumn.EditType = EditType.cascader.ToString();
                                dtoColumn.CascaderValueName = attr.CascaderValueName;
                                var customOption = customOptions.FirstOrDefault(o => o.PropertyName == property.Name);
                                if (customOption != null)
                                {
                                    dtoColumn = customOption.Func(dtoColumn);
                                }
                            }
                            if (attr.EditType == EditType.multiple)
                            {
                                dtoColumn.EditType = EditType.multiple.ToString();
                                dtoColumn.MultipleValueName = attr.MultipleValueName;
                                //if (!string.IsNullOrWhiteSpace(dtoColumnRequest.ExternalIds))
                                //{
                                //    attr.OptionSql += $" and id in ({dtoColumnRequest.ExternalIds.Trim(',')})";
                                //}
                                var customOption = customOptions.FirstOrDefault(o => o.PropertyName == property.Name);
                                if (customOption != null)
                                {
                                    dtoColumn = customOption.Func(dtoColumn);
                                }
                            }

                            showColumns.Add(dtoColumn);
                            sort++;
                        }
                        else
                        {
                            showColumns.Add(new DtoColumn()
                            {
                                PropertyName = property.Name,
                                ShowName = attr.ShowName,
                                SortId = attr.SortId > 0 ? attr.SortId : sort,
                                ControlHtml = GetControlHtml(attr).ToString(),
                                EditType = (EditType.hidden).ToString(),
                                CascaderValueName = attr.CascaderValueName
                            });
                            sort++;
                        }
                    }
                }
            }
            return showColumns.OrderBy(o => o.SortId).ToList();
        }

        internal StringBuilder GetControlHtml(DtoColumnAttribute attr)
        {
            var sb = new StringBuilder();
            switch (attr.EditType)
            {
                case EditType.text:
                    sb.Append(@"");
                    break;
            }
            return sb;
        }
    }
}
