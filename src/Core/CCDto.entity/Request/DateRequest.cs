using System;
using System.Collections.Generic;
using System.Text;

namespace CCDto.entity.Request
{
    /// <summary>
    /// 时间段请求参数
    /// </summary>
    public class DateRequest:BaseRequest
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }


        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

    }
}
