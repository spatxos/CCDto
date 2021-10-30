using CCDto.entity.DtoColumn;
using CCDto.entity.DtoColumn.Option;
using FreeSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace CCDto.entity.Base
{
    [AttributeUsage(AttributeTargets.Property)]

    public class DtoColumnAttribute : Attribute
    {
        public bool IsDisabled { get; set; }

        public bool IsEdit { get; set; }

        public EditType EditType { get; set; }

        public string ShowName { get; set; }

        public int SortId { get; set; }

        public string OptionSql { get; set; }

        /// <summary>
        /// Expression<Func<Type, bool>>
        /// </summary>
        public object ExpressionQuery { get; set; }


        public bool IsSplice { get; set; }

        public Type Type { get; set; }

        public string CascaderValueName { get; set; }

        public string MultipleValueName { get; set; }

        public string TypeName { get; set; }

        public DtoColumnAttribute()
        {
            EditType = EditType.text;
            IsEdit = true;
        }

        public void SetExpressionQuery()
        { 
        
        }
    }
}
