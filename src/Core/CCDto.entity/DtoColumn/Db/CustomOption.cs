using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCDto.entity.DtoColumn.Db
{
    public class CustomOption
    {
        public string PropertyName { get; set; }
        public Func<DtoColumn, DtoColumn> Func { get; set; }
    }
}
