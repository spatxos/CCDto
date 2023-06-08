using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Template.Controllers
{
    /// <summary>
    /// 健康检查
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]

    public class HealthController : ControllerBase
    {
        private readonly ILogger<HealthController> _logger;
        public HealthController(ILogger<HealthController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 健康检查
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public bool Check()
        {
            return true;
        }
    }
}
