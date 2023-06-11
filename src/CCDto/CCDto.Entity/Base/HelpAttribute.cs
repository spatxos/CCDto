using System;
using System.Collections.Generic;
using System.Text;

namespace CCDto.Entity.Base
{
    public class HelpAttribute : Attribute
    {
        public HelpAttribute(string Description_in)
        {
            this._description = Description_in;
        }

        protected string _description;

        public string Description
        {
            get
            {
                return this._description;
            }
        }
    }
}
