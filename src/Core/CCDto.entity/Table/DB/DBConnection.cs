using CCDto.entity.Base;
using CCDto.entity.FreeSql;
using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCDto.entity.Table
{
    /// <summary>
    /// 数据库连接
    /// </summary>
    [Serializable]
    [Table(Name = "TBL_DBCONNECTION",OldName = "DBConnection")]
    [MultiDB(DbName: "DB1")]
    public class DBConnection : Entity<int>
    {
        /// <summary>
        /// 数据库名称
        /// </summary>
        public virtual string DBConnectionName { get; set; }

        /// <summary>
        /// 数据库编号
        /// </summary>
        public virtual string DBConnectionNo { get; set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public virtual int DBType { get; set; }


        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public virtual string DBConnectionString { get; set; }

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
