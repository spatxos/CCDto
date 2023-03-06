using CCDto.entity.Base;
using CCDto.entity.DtoColumn;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCDto.entity.Dto
{
    /// <summary>
    /// A shortcut of <see cref="EntityDto{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    [Serializable]
    public class EntityDto : EntityDto<int>, IEntityDto
    {
        /// <summary>
        /// Creates a new <see cref="EntityDto"/> object.
        /// </summary>
        public EntityDto()
        {

        }

        /// <summary>
        /// Creates a new <see cref="EntityDto"/> object.
        /// </summary>
        /// <param name="id">Id of the entity</param>
        public EntityDto(int id)
            : base(id)
        {
        }
    }

    /// <summary>
    /// Implements common properties for entity based DTOs.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key</typeparam>
    [Serializable]
    public class EntityDto<TPrimaryKey> : IEntityDto<TPrimaryKey>
    {
        /// <summary>
        /// Id of the entity.
        /// </summary>
        [DtoColumn(IsDisabled = true,IsEdit = false)]
        public virtual TPrimaryKey Id { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        [DtoColumn(ShowName = "删除标识", IsDisabled = true, IsEdit = false, EditType = EditType.boolselect)]
        public virtual bool IsDelete { get; set; }
        /// <summary>
        /// 删除标识
        /// </summary>
        [DtoColumn(ShowName = "删除标识", IsDisabled = true, IsEdit = false)]
        public virtual string IsDeleteValue => IsDelete ? "是" : "否";

        /// <summary>
        /// Creates a new <see cref="EntityDto{TPrimaryKey}"/> object.
        /// </summary>
        public EntityDto()
        {

        }

        /// <summary>
        /// Creates a new <see cref="EntityDto{TPrimaryKey}"/> object.
        /// </summary>
        /// <param name="id">Id of the entity</param>
        public EntityDto(TPrimaryKey id)
        {
            Id = id;
        }
    }

}
