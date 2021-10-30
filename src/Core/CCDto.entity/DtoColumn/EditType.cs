using System;
using System.Collections.Generic;
using System.Text;

namespace CCDto.entity.DtoColumn
{
    public enum EditType
    {
        text = 0,
        select = 1,
        WdatePicker = 2,
        boolselect = 3,
        statusselect = 4,
        enumselect = 5,
        cascader = 6,
        reflectselect = 7,
        multiple = 8,

        hidden = 99
    }
}
