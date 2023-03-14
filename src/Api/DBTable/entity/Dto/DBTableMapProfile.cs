using AutoMapper;
using CCDto.entity;
using System;
using System.Collections.Generic;
using System.Text;
using CCDto.entity.Table;
using api.dbtable.entity;
using CCDto.common.AutoMapper;

namespace api.dbtable.entity.Dto
{
    public class DBTableMapProfile : Profile, IProfile
    {
        public DBTableMapProfile()
        {
            CreateMap<DBTable, DBTableDto>().AfterMap((from, to) => { 
                to.CreateTime = from.CreateTime == null ? DateTime.Now : from.CreateTime;
                to.DBConnectionName = from.DBConnection == null ? "" : from.DBConnection.DBConnectionName;

                try
                {
                    //if (from.DBField != null)
                    //{
                    //    to.DBFieldName = from.DBField == null ? "" : from.DBField.DBTable.DBTableName + "/" + from.DBField.DBFieldName;
                    //    to.DBFieldIds = new int[3] { from.DBField.DBTable.DBConnectionId, from.DBField.DBTableId, from.DBField.Id };
                    //}
                    //else
                    //{
                        to.DBFieldIds = new int[3] { 0, 0, 0 };
                    //}
                }
                catch
                {
                    to.DBFieldIds = new int[3] { 0, 0, 0 };
                }
            });
            CreateMap<DBTableDto, DBTable>().AfterMap((from, to) => { to.CreateTime = from.CreateTime == null ? DateTime.Now : from.CreateTime; });
            CreateMap<List<DBTable>, List<DBTableDto>>();
        }
    }
}
