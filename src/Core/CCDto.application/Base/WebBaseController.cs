using CCDto.entity.Table;
using CCDto.entity.Request;

using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CCDto.application.Base
{
    public class WebBaseController : BaseController
    {
        public WebBaseController()
        {
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //var isRedirect = true;

            //var LoginUser = new User();
            //if (HttpContext.User.Identity.IsAuthenticated)
            //{
            //    string LoginUserStr = "";
            //    var claims = HttpContext.User.Claims.Where(o => o.Type == "LoginUser");
            //    if (claims != null)
            //    {
            //        LoginUserStr = claims.FirstOrDefault().Value;
            //        if (!string.IsNullOrWhiteSpace(LoginUserStr))
            //        {
            //            isRedirect = false;
            //            ViewBag.LoginUser = LoginUser = JsonConvert.DeserializeObject<User>(LoginUserStr);
            //        }
            //    }
            //}

            //if (isRedirect)
            //{
            //    filterContext.Result = Redirect($"/Account/Logout");
            //}
            base.OnActionExecuting(filterContext);

        }
    }
}
