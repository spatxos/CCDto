using CCDto.entity.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCDto.entity.Request
{
    public class LoginRequest: BaseRequest
    {
        public string username { get; set; }
        public string password { get; set; }

    }
}
