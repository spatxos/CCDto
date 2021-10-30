using CCDto.application.Service.Crud.Dto;
using CCDto.entity;
using CCDto.entity.Base;
using CCDto.entity.DtoColumn;
using FreeSql.DataAnnotations;
using System;
using CCDto.entity.Table;
using System.Linq.Expressions;

namespace CCDto.application.Service.DBTables.Dto
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

        [DtoColumn(ShowName = "数据库", IsDisabled = true, EditType = EditType.select, OptionSql = "select Id,DBConnectionName value from TBL_DBCONNECTION where isdelete=0")]
        public virtual int DBConnectionId { get; set; }

        [DtoColumn(ShowName = "数据库",IsEdit = false)]
        public virtual string DBConnectionName { get; set; }

        [DtoColumn(ShowName = "添加时间", IsEdit = false)]
        public DateTime? CreateTime { get; set; }

        [DtoColumn(ShowName = "关联自动化字段", IsDisabled = true, EditType = EditType.cascader, CascaderValueName = "DBFieldIds", OptionSql = @"select t1.id id1,t1.DBConnectionName name1,t2.id id2,t2.DBTableName name2,t3.Id id3,t3.DBFieldName name3  from TBL_DBCONNECTION t1 left join TBL_DBTABLE t2 on t1.id = t2.DBConnectionId left join TBL_DBFIELD t3 on t2.id = t3.DBTableId WHERE t1.IsDelete = 0 and t2.IsDelete = 0 and t3.IsDelete = 0 ")]
        public virtual int DBFieldId { get; set; }

        [DtoColumn(ShowName = "关联自动化字段", IsDisabled = true, EditType = EditType.hidden)]
        public int[] DBFieldIds { get; set; }

        [DtoColumn(ShowName = "关联自动化字段", IsEdit = false)]
        public string DBFieldName { get; set; }

        [DtoColumn(ShowName = "备注")]
        public string Remark { get; set; }

    }

}
