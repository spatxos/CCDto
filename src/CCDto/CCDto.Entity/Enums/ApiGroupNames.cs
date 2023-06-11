using CCDto.Entity.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCDto.Entity.Enums
{
    /// <summary>
    /// 接口分组枚举
    /// </summary>
    public enum ApiGroupNames
    {
        [GroupInfo(Title = "登录认证", Description = "登录相关接口", Version = "v1")]
        Login,
        [GroupInfo(Title = "User", Description = "用户相关接口")]
        User,
        [GroupInfo(Title = "Order", Description = "订单相关接口")]
        Order
    }
}
