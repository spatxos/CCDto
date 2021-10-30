using AutoMapper;
using com.msung.common.AutoMapper;
using com.msung.entity;
using System;
using System.Collections.Generic;
using System.Text;
using com.msung.entity.Table;
using com.msung.entity.Table.TableSpace;

namespace com.msung.application.Service.TableNames.Dto
{
    public class TableNameMapProfile : Profile, IProfile
    {
        public TableNameMapProfile()
        {
            CreateMap<TableName, TableNameDto>().AfterMap((from, to) => { to.CreateTime = from.CreateTime == null ? DateTime.Now : from.CreateTime; });
            CreateMap<TableNameDto, TableName>().AfterMap((from, to) => { to.CreateTime = from.CreateTime == null ? DateTime.Now : from.CreateTime; });
            CreateMap<List<TableName>, List<TableNameDto>>();
        }
    }
}
