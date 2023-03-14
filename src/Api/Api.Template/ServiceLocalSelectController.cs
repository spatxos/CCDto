using System;
using System.Reflection;
using CCDto.entity.Base;
using Panda.DynamicWebApi;
using Panda.DynamicWebApi.Attributes;

namespace Api.Template
{
    internal class ServiceLocalSelectController : ISelectController
    {
        public bool IsController(Type type)
        {
            var isController = type.IsPublic && type.GetCustomAttribute<ServiceAttribute>() != null;
            Console.WriteLine($"{type.FullName},isController:{isController}");
            return type.IsPublic && type.GetCustomAttribute<ServiceAttribute>() != null;
        }
    }
}