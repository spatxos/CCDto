using CCDto.common;
using CCDto.common.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CCDto.common.NetCore.Extensions
{
    public class ProfileFactory
    {
        public List<Type> ConvertList { get; } = new List<Type>();

        public void AddAssemblys(params Assembly[] assemblys)
        {
            foreach (var assembly in assemblys)
            {
                ConvertList.AddRange(assembly.GetTypes()
                    .Where(type =>
                     typeof(IProfile).GetTypeInfo().IsAssignableFrom(type) && type != typeof(IProfile)).ToArray());
            }
        }
    }
}
