using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Panda.DynamicWebApi;
using Panda.DynamicWebApi.Attributes;
using System;
using System.Reflection;

namespace Api.Template
{
    internal class ServiceActionRouteFactory : IActionRouteFactory
    {
        public string CreateActionRouteModel(string areaName, string controllerName, ActionModel action)
        {
            var controllerType = action.ActionMethod.DeclaringType;
            var serviceAttribute = controllerType.GetCustomAttribute<ServiceAttribute>();
            var serviceAttributeName = controllerName;

            if (serviceAttribute != null) {
                serviceAttributeName = serviceAttribute.ServiceName;
            }

            var _controllerName = serviceAttributeName == string.Empty ? controllerName.Replace("Service", "") : serviceAttributeName.Replace("Service", "");
            Console.WriteLine($"api/{_controllerName}/{action.ActionName.Replace("Async", "")}");

            return $"api/{_controllerName}/{action.ActionName.Replace("Async", "")}";
        }
    }
}