using AutoMapper;
using CCDto.common.AutoMapper;
using CCDto.entity;
using System;
using System.Collections.Generic;
using System.Text;
using CCDto.entity.Table;
using api.dbconnecion.entity;

namespace dbconnecion.application.Dto
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
