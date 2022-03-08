using CCDto.entity.Base;
using CCDto.entity.FreeSql;
using FreeSql.DataAnnotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCDto.entity.Table
{
    /// <summary>
    /// 数据库表字段
    /// </summary>
    [Serializable]
    [Table(Name = "TBL_DBFIELD", OldName = "DBField")]
    [MultiDB(DbName: "DB3")]
    public class DBField : Entity<int>
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public virtual string DBFieldName { get; set; }
        /// <summary>
        /// 字段编号
        /// </summary>
        public virtual string DBFieldNo { get; set; }

        /// <summary>
        /// 表Id
        /// </summary>
        public virtual int DBTableId { get; set; }

        public virtual DBTable DBTable { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? CreateTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }

    }

}
