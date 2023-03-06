using AutoMapper;
using CCDto.common.AutoMapper;
using CCDto.entity;
using System;
using System.Collections.Generic;
using System.Text;
using CCDto.entity.Table;
using api.dbfield.entity;

namespace api.dbfield.application.Dto
{
    public class DBFieldMapProfile : Profile, IProfile
    {
        public DBFieldMapProfile()
        {
            CreateMap<DBField, DBFieldDto>().AfterMap((from, to) => {
                to.CreateTime = from.CreateTime == null ? DateTime.Now : from.CreateTime;
                //to.DBTableName = from.DBTable == null ? "" : from.DBTable.DBTableName;
            });
            CreateMap<DBFieldDto, DBField>().AfterMap((from, to) => { to.CreateTime = from.CreateTime == null ? DateTime.Now : from.CreateTime; });
            CreateMap<List<DBField>, List<DBFieldDto>>();
        }
    }
}
