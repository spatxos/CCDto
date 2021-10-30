using AutoMapper;
using CCDto.common.AutoMapper;
using CCDto.entity.Table;

namespace CCDto.application.Service.Nav.Dto
{
    public class MenuMapProfile : Profile, IProfile
    {
        public MenuMapProfile()
        {
            CreateMap<Menus, MenuDto>().ForMember(d => d.Childs, opt => opt.Ignore());

        }
    }
}
