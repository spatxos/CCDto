using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCDto.entity.DtoColumn.Option
{
    [Table(DisableSyncStructure = true)]
    public class MultipleOption
    {
        public int value { get; set; }
        public string label { get; set; }

    }
}
