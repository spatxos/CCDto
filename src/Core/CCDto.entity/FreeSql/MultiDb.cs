using FreeSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCDto.entity.FreeSql
{
    /// <summary>
    /// 多数据库
    /// </summary>
    public class MultiDb
    {
        /// <summary>
        /// 数据库命名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public DataType DbType { get; set; }

        /// <summary>
        /// 数据库字符串
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
