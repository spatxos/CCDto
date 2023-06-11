using CCDto.Entity.Enums;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCDto.Entity.Attributes
{
    /// <summary>
    /// 分组信息特性
    /// </summary>
    public class GroupInfoAttribute : Attribute
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// 分组接口特性
    /// </summary>
    public class ApiGroupAttribute : Attribute, IApiDescriptionGroupNameProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        //public ApiGroupAttribute(params ApiGroupNames[] name)
        //{
        //    GroupName = string.Join(",", name.Select(o=>o.ToString())));
        //}
        public ApiGroupAttribute(ApiGroupNames name)
        {
            GroupName = name.ToString();
        }
        /// <summary>
        /// 分组名称
        /// </summary>
        public string GroupName { get; set; }
    }
}
