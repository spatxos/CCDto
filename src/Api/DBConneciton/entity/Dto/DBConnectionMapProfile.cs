using AutoMapper;
using CCDto.common.AutoMapper;
using System;
using System.Collections.Generic;

namespace api.dbconnecion.entity.Dto
{
    public class DBConnectionMapProfile : Profile, IProfile
    {
        public DBConnectionMapProfile()
        {
            CreateMap<DBConnection, DBConnectionDto>().AfterMap((from, to) => { to.CreateTime = from.CreateTime == null ? DateTime.Now : from.CreateTime; });
            CreateMap<DBConnectionDto, DBConnection>().AfterMap((from, to) => { to.CreateTime = from.CreateTime == null ? DateTime.Now : from.CreateTime; });
            CreateMap<List<DBConnection>, List<DBConnectionDto>>();
        }
    }
}
