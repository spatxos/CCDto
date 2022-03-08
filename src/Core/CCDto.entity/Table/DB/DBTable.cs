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
    /// 数据库表
    /// </summary>
    [Serializable]
    [Table(Name = "TBL_DBTABLE", OldName = "DBTable")]
    [MultiDB(DbName: "DB2")]
    public class DBTable : Entity<int>
    {
        /// <summary>
        /// 数据库表名称
        /// </summary>
        public virtual string DBTableName { get; set; }
        /// <summary>
        /// 数据库表编号
        /// </summary>
        public virtual string DBTableNo { get; set; }

        /// <summary>
        /// 数据库Id
        /// </summary>
        public virtual int DBConnectionId { get; set; }

        public virtual DBConnection DBConnection { get; set; }

        [JsonIgnore]
        public virtual List<DBField> DBFields { get; set; }

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
