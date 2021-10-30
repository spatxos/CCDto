using CCDto.entity.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCDto.entity.Request.EditPassword
{
    public class EditPasswordRequest : BaseRequest
    {
        public int Id { get; set; }
        public string Password1 { get; set; }

        public string Password2 { get; set; }

    }
}
