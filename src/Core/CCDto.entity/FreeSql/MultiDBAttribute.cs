using System;
using System.Collections.Generic;
using System.Text;

namespace CCDto.entity.FreeSql
{
    public class MultiDBAttribute : Attribute
    {
        public MultiDBAttribute(string DbName)
        {
            this._dbName = DbName;
        }

        protected string _dbName;

        public string DbName
        {
            get
            {
                return this._dbName;
            }
        }
    }
}
