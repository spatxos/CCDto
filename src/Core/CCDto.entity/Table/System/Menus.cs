
using CCDto.entity.Base;
using FreeSql.DataAnnotations;

namespace CCDto.entity.Table
{
    /// <summary>
    /// TBL_NAVITREE:实体类(导航菜单信息表)
    /// </summary>
    public class Menus : Entity<int>
    {
        /// <summary>
        /// 菜单名
        /// </summary>
        public string MenuName { get; set; }
        /// <summary>
        /// URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 上级Id
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

    }
}
