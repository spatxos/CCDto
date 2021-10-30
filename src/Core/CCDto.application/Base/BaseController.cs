using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using CCDto.entity.Base;

namespace CCDto.application.Base
{
    public class BaseController : Controller, IActionFilter
    {
        public string SystemName = "";

        public ReturnMsg returnMsg = new ReturnMsg(false);

    }
}
