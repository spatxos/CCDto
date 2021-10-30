using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCDto.entity.DtoColumn.Option
{
    [Table(DisableSyncStructure = true)]
    public class Option
    {
        public int Id { get; set; }

        public string Value { get; set; }

    }
}
