using CCDto.entity.DtoColumn.Option;
using FreeSql;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CCDto.entity.DtoColumn
{
    public class DtoColumn
    {
        public string ShowName { get; set; }
        public string PropertyName { get; set; }

        public string ControlHtml { get; set; }

        public int SortId { get; set; }

        public string EditType { get; set; }

        public List<(object, object)> Options { get; set; }

        public List<CascaderOption> CascaderOptions { get; set; }

        public string CascaderValueName { get; set; }

        public List<MultipleOption> MultipleOptions { get; set; }

        public string MultipleValueName { get; set; }

        public Expression<Func<Type, bool>> Select { get; set; }

        public DtoColumn()
        {
            Options = new List<(object, object)>();
            CascaderOptions = new List<CascaderOption>();
            MultipleOptions = new List<MultipleOption>();
        }
    }
}

