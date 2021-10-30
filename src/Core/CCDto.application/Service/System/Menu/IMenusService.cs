using CCDto.application.Base;
using CCDto.application.Service.Crud;
using CCDto.application.Service.Nav.Dto;
using CCDto.entity.Table;
using FreeSql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CCDto.application.Service.Nav
{
    public interface IMenusService : IAsyncCrudAppService<Menus, MenuDto, int, MenusPagedResultRequestDto, MenuDto>
    {
        /// <summary>
        /// 获取导航菜单
        /// </summary>
        /// <returns></returns>
        List<MenuDto> GetMenusTrees();

        string GetScript();
    }
}
