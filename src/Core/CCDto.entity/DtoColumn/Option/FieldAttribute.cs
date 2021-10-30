using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCDto.entity.DtoColumn.Option
{
    [Table(DisableSyncStructure = true)]
    public class FieldAttribute
    {
        public string FieldName { get; set; }

        public string FieldValue { get; set; }

        public string Description { get; set; }

    }
}
