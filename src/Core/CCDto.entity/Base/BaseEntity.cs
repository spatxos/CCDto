using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCDto.entity.Base
{
    [Serializable]
    public class BaseEntity
    {
        public override string ToString()
        {
            try
            {
                return JsonConvert.SerializeObject(this);
            }
            catch
            {
                return "";
            }
        }
    }

}
