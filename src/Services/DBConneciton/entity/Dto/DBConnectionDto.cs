using CCDto.Entity.Base;
using CCDto.Entity.Dto;
using System;

namespace api.dbconnecion.entity.Dto
{

    public class DBConnectionDto:EntityDto<int>
    {
        //DtoColumn 查询列表不显示 ，设置IsDisabled = true 
        //DtoColumn 新增/修改 不显示 ，设置IsEdit = false 
        //DtoColumn 新增/修改 是选择框 ，设置EditType = EditType.select 
        //DtoColumn 外联从OptionValue字典表读取 ，设置OptionGroup = "你的OptionGroup名称" 
        //DtoColumn 外联从其他表读取 分为 
        //1.新增/修改中的select 设置OptionSql = "select id,name as value from tablename where ..." 
        //2.列表显示中  先在Entity中增加对应表实体，Dto中增加对应Value或者Name名称，MapProfile中增加对应映射关系

        public virtual string DBConnectionName { get; set; }
        public virtual string DBConnectionNo { get; set; }

        public virtual int DBType { get; set; }


        public virtual string DBConnectionString { get; set; }

        public DateTime? CreateTime { get; set; }

        public string Remark { get; set; }

    }

}
