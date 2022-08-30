using CCDto.entity.DtoColumn;
using CCDto.entity.DtoColumn.Db;
using CCDto.entity.DtoColumn.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCDto.common.Option
{
    public static class OptionHelper
    {
        #region 处理Cascader的select
        /// <summary>
        /// 处理Cascader的select
        /// </summary>
        /// <param name="cascaders"></param>
        /// <returns></returns>
        public static List<CascaderOption> DealwithCascader(List<DbCascader> cascaders)
        {
            var cascaderOptions = new List<CascaderOption>();
            if (cascaders.Any())
            {
                foreach (var db in cascaders.Select(o => new { o.id1, o.name1 }).Distinct())
                {
                    var dbcascaderOption = new CascaderOption()
                    {
                        label = db.name1,
                        value = db.id1,
                        children = new List<CascaderOption>()
                    };
                    foreach (var table in cascaders.Where(o => o.id1 == db.id1).Select(o => new { o.id2, o.name2 }).Distinct())
                    {
                        var tablecascaderOption = new CascaderOption()
                        {
                            label = table.name2,
                            value = table.id2,
                            children = new List<CascaderOption>()
                        };
                        tablecascaderOption.children.Add(new CascaderOption()
                        {
                            label = "不选择",
                            value = 0,
                            children = null
                        });

                        foreach (var fieid in cascaders.Where(o => o.id1 == db.id1 && o.id2 == table.id2).Select(o => new { o.id3, o.name3 }).Distinct())
                        {
                            var fieidcascaderOption = new CascaderOption()
                            {
                                label = fieid.name3,
                                value = fieid.id3,
                                children = null
                            };
                            tablecascaderOption.children.Add(fieidcascaderOption);
                        }
                        dbcascaderOption.children.Add(tablecascaderOption);
                    }
                    cascaderOptions.Add(dbcascaderOption);
                }
            }
            return cascaderOptions;
        }

        #endregion

        #region 处理Multiple的select
        /// <summary>
        /// 处理Multiple的select
        /// </summary>
        /// <param name="multiples"></param>
        /// <returns></returns>
        public static List<MultipleOption> DealwithMultiple(List<DbMultiple> multiples)
        {
            var multipleOptions = new List<MultipleOption>();
            if (multiples.Any())
            {
                foreach (var db in multiples)
                {
                    var multipleOption = new MultipleOption()
                    {
                        label = db.label,
                        value = db.value,
                    };
                    multipleOptions.Add(multipleOption);
                }
            }
            return multipleOptions;
        }

        #endregion

    }
}
