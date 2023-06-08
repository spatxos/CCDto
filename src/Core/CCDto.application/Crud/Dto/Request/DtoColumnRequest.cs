using System;
using System.Collections.Generic;
using System.Text;

namespace CCDto.BaseService.Crud.Dto.Request
{
    public class DtoColumnRequest
    {
        public bool IsCheckDisabled { get; set; }
        public bool IsCheckEdit { get; set; }

        public int QueryId { get; set; }

        public string ExternalIds { get; set; }
    }
}
