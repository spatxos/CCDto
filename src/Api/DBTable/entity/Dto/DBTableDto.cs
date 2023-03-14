using CCDto.entity.Base;
using CCDto.entity.DtoColumn;
using System;
using CCDto.entity.Dto;

namespace api.dbtable.entity.Dto
{

    public class DBTableDto : EntityDto<int>
    {
        //DtoColumn 查询列表不显示 ，设置IsDisabled = true 
        //DtoColumn 新增/修改 不显示 ，设置IsEdit = false 
        //DtoColumn 新增/修改 是选择框 ，设置EditType = EditType.select 
        //DtoColumn 外联从OptionValue字典表读取 ，设置OptionGroup = "你的OptionGroup名称" 
        //DtoColumn 外联从其他表读取 分为 
        //1.新增/修改中的select 设置OptionSql = "select id,name as value from tablename where ..." 
        //2.列表显示中  先在Entity中增加对应表实体，Dto中增加对应Value或者Name名称，MapProfile中增加对应映射关系

        [DtoColumn(ShowName = "数据库表名")]
        public virtual string DBTableName { get; set; }

        [DtoColumn(ShowName = "数据库表编号")]
        public virtual string DBTableNo { get; set; }

        [DtoColumn(ShowName = "数据库", IsDisabled = true, EditType = EditType.select)]
        public virtual int DBConnectionId { get; set; }

        [DtoColumn(ShowName = "数据库",IsEdit = false)]
        public virtual string DBConnectionName { get; set; }

        [DtoColumn(ShowName = "添加时间", IsEdit = false)]
        public DateTime? CreateTime { get; set; }

        [DtoColumn(ShowName = "关联自动化字段", IsDisabled = true, EditType = EditType.cascader, CascaderValueName = "DBFieldIds")]
        public virtual int DBFieldId { get; set; }

        [DtoColumn(ShowName = "关联自动化字段", IsDisabled = true, EditType = EditType.hidden)]
        public int[] DBFieldIds { get; set; }

        [DtoColumn(ShowName = "关联自动化字段", IsEdit = false)]
        public string DBFieldName { get; set; }

        [DtoColumn(ShowName = "备注")]
        public string Remark { get; set; }

    }

}
