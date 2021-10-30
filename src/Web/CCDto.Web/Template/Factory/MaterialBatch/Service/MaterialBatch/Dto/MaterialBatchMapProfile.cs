using AutoMapper;
using com.msung.common.AutoMapper;
using com.msung.entity;
using System;
using System.Collections.Generic;
using System.Text;
using com.msung.entity.Table;
using com.msung.entity.Table.Factory;

namespace com.msung.application.Service.MaterialBatchs.Dto
{
    public class MaterialBatchMapProfile : Profile, IProfile
    {
        public MaterialBatchMapProfile()
        {
            CreateMap<MaterialBatch, MaterialBatchDto>().AfterMap((from, to) => { to.CreateTime = from.CreateTime == null ? DateTime.Now : from.CreateTime; });
            CreateMap<MaterialBatchDto, MaterialBatch>().AfterMap((from, to) => { to.CreateTime = from.CreateTime == null ? DateTime.Now : from.CreateTime; });
            CreateMap<List<MaterialBatch>, List<MaterialBatchDto>>();
        }
    }
}
