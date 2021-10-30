using FreeSql.DataAnnotations;
using System;

namespace CCDto.entity.Base
{
    /// <summary>
    /// Defines interface for base entity type. All entities in the system must implement this interface.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
    //
    public interface IEntity<TPrimaryKey>
    {
        /// <summary>
        /// Unique identifier for this entity.
        /// </summary>
        [Column(IsPrimary = true, IsIdentity = true)]
        TPrimaryKey Id { get; set; }

        /// <summary>
        /// 逻辑删除.
        /// </summary>
        [DtoColumn(IsDisabled = false, IsEdit = false)]
        public bool IsDelete { get; set; }

        /// <summary>
        /// Checks if this entity is transient (not persisted to database and it has not an <see cref="Id"/>).
        /// </summary>
        /// <returns>True, if this entity is transient</returns>
        bool IsTransient();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool IsAboveZero();
    }
}
