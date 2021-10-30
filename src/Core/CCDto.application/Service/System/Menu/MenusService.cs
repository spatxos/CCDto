using CCDto.application.Service.Crud;
using CCDto.application.Service.Nav.Dto;
using CCDto.common.AutoMapper;
using CCDto.entity.Table;
using FreeSql;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;

namespace CCDto.application.Service.Nav
{
    public class MenusService : AsyncCrudAppService<Menus, MenuDto, int,MenusPagedResultRequestDto, MenuDto>, IMenusService
    {

        public MenusService(IFreeSql fsql) : base(fsql)
        {
            
        }

        #region 获取导航菜单
        /// <summary>
        /// 获取导航菜单
        /// </summary>
        /// <returns></returns>
        public List<MenuDto> GetMenusTrees()
        {
            var navs = GetAll().OrderBy(o=>o.Order).ToList();
            return SetNaviTrees(navs, 0);
        }
        #endregion

        public string GetScript()
        {
            var sb = new StringBuilder("$('#page-sidebar-menu-bs-navbar-ul').append(");

            sb.AppendLine(@""" """);

            var dtos = GetMenusTrees();

            foreach(var dto in dtos)
            {
                sb.AppendLine(@$"+""<li class='heading'>""+
                               ""<h3 class='uppercase'>{dto.MenuName}</h3>""+
                           ""</li>""");

                if (dto.Childs.Any())
                {
                    sb.AppendLine(GetChildScript(dto.Childs));
                }
                sb.AppendLine(@"+""</li>""");
            }
            sb.AppendLine(@");");
            return sb.ToString();
        }

        internal string GetChildScript(List<MenuDto> dtos)
        {
            var sb = new StringBuilder();

            string model_nl = @"+""<li class='nav-item open'>""
                              +""<a href='{1}' class='nav-link nav-toggle'>""+
                              ""<i class='icon-diamond'></i>""+
                              ""<span class='title'>{0}</span>""+
                              ""<span class='arrow open'></span>""+
                              ""</a><ul class='sub-menu' style='display: block; '>""
                              {2}
                             +""</ul></li>""";

             //model_nl = @" + ""<li class='nav-link nav-toggle'>""+
             //                ""<a data-toggle='dropdown' class='dropdown-toggle' data-hover='dropdown' data-delay='200' href='{1}'>""+
             //                   "" {0}""+
             //                    ""<b class='caret'></b>""+
             //                ""</a>""+
             //                ""<ul aria-labelledby='drop6' role='menu' class='dropdown-menu'>""
             //                  {2}
             //                +""</ul></li>""";

            string model_li = @"
                       +""<li  class='nav-item'>""+
                       "" <a href='{1}'><i class='{2}'></i><span class='title'>{0}</span></a>""+
                      "" </li>"" ";

            foreach (var dto in dtos)
            {
                if (!dto.Childs.Any())
                {
                    sb.AppendLine(string.Format(model_li, dto.MenuName, dto.Url, dto.Icon));
                }
                else
                {
                    if (dto.Childs.Any())
                    {
                        sb.AppendLine(string.Format(model_nl, dto.MenuName, dto.Url, GetChildScript(dto.Childs)));
                    }
                    else
                    {
                        sb.AppendLine(string.Format(model_li, dto.MenuName, dto.Url, dto.Icon));
                    }
                }
            }

            return sb.ToString();
        }

        internal List<MenuDto> SetNaviTrees(List<Menus> navs,int pid)
        {
            var dtos = new List<MenuDto>();
            var nownavs = navs.Where(o => o.ParentId == pid).ToList();
            foreach (var nav in navs.Where(o=>o.ParentId == pid))
            {
                var dto = nav.MapTo<MenuDto>();

                if (dto != null)
                {
                    dto.Childs = SetNaviTrees(navs, dto.Id);
                }
                dtos.Add(dto);
            }
            
            return dtos;
        }
    }
}
