using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCDto.entity.DtoColumn.Db
{
    [Table(DisableSyncStructure = true)]
    public class DbCascader
    {
        public int id1 { get; set; }

        public string name1 { get; set; }

        public int id2 { get; set; }

        public string name2 { get; set; }

        public int id3 { get; set; }

        public string name3 { get; set; }

    }
}
