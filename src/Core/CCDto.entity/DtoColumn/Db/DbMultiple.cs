using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCDto.entity.DtoColumn.Db
{
    [Table(DisableSyncStructure = true)]
    public class DbMultiple 
    {
        public string label { get; set; }

        public int value { get; set; }

    }
}
