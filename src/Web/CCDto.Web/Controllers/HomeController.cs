using System.Diagnostics;
using CCDto.application.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CCDto.Web.Controllers
{
    public class HomeController : WebBaseController
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return Redirect("~/DB/DBConnection/Index");
        }
    }
}
