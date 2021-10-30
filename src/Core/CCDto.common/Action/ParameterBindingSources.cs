using System;
using System.Collections.Generic;
using System.Text;

namespace CCDto.common.Action
{
    public static class ParameterBindingSources
    {
        public const string ModelBinding = "ModelBinding";
        public const string Query = "Query";
        public const string Body = "Body";
        public const string Path = "Path";
        public const string Form = "Form";
        public const string Header = "Header";
        public const string Custom = "Custom";
        public const string Services = "Services";
    }
}
