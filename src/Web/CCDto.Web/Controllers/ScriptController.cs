using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using CCDto.application.Service.Nav;
using CCDto.application.Base;

namespace CollectionConfiguration.Controllers
{
    public class ScriptController : WebBaseController
    {
        private readonly ILogger<ScriptController> _logger;
        private readonly IMenusService _menuService;
        public ScriptController(ILogger<ScriptController> logger,IMenusService menuService)
        {
            _logger = logger;
            _menuService = menuService;
        }

        public ContentResult GetScript()
        {
            var script = new StringBuilder();
            script.AppendLine("(function(){");

            try
            {
                //var i =  _navService.();
                script.AppendLine(_menuService.GetScript());
                script.AppendLine();
                script.AppendLine("$('#page-wrapper-page-footer').show();");
                script.AppendLine("$('#page-wrapper-page-quick-sidebar-wrapper').show();");
                script.AppendLine("$('#page-wrapper-page-sidebar-wrapper').show();");
                script.AppendLine("$('#page-wrapper-page-header').show();");

            }
            catch {
                script.AppendLine("$('.page-header-fixed .page-container').css('margin-top', '1px');");
                script.AppendLine("$('.page-content').css('margin-left','1px');");
            }
            script.Append("})();");

            return Content(script.ToString(), "application/x-javascript", Encoding.UTF8);
        }
    }
}
