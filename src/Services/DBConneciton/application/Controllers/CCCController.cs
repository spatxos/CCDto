using CCDto.Entity.Attributes;
using CCDto.Entity.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace api.dbconnecion.application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CCCController : ControllerBase
    {
        private readonly ILogger<CCCController> _logger;
        public CCCController(ILogger<CCCController> logger)
        {
            _logger = logger;
        }

        [HttpGet("[action]")]
        [ApiGroup(ApiGroupNames.User)]

        public string GetAssembly()
        {
            return Assembly.GetExecutingAssembly().FullName;
        }

        [HttpGet("[action]")]
        [ApiGroup(ApiGroupNames.Login)]

        public string GetAssembly1()
        {
            return Assembly.GetExecutingAssembly().FullName;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("home/getuser")]
        [ApiGroup(ApiGroupNames.User)]
        public string GetUser()
        {
            return "my name is dotnetboy";
        }
        /// <summary>
        /// 登录成功
        /// </summary>
        /// <returns></returns>
        [HttpPost("home/login")]
        [ApiGroup(ApiGroupNames.Login)]
        public string Login()
        {
            return "login success";
        }
        /// <summary>
        /// 删除订单
        /// </summary>
        [HttpDelete("home/{id}")]
        [ApiGroup(ApiGroupNames.Order)]
        public string DeleteOrder(string id)
        {
            return $"delete success,id={id}";
        }
        /// <summary>
        /// 留言
        /// </summary>
        [HttpDelete("home/message")]
        public string DeleteUser(string msg)
        {
            return $"message：{msg}";
        }
    }
}
