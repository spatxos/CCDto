using CCDto.common;
using CCDto.common.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CCDto.Web.Core.AutoMapper
{
    public static class MapperRegister
    {
        public static Type[] MapType(string assemblyName)
        {
            return RuntimeHelper.GetAssemblyByName(assemblyName).GetTypes()
               .Where(type =>
                typeof(IProfile).GetTypeInfo().IsAssignableFrom(type)).ToArray(); 

        }

    }
}
