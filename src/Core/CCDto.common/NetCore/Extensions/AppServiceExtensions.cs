using CCDto.common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCDto.common.NetCore.Extensions
{
    public static class AppServiceExtensions
    {
        public static void AddAssembly(this IServiceCollection service, string assemblyName , ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            var assembly = RuntimeHelper.GetAssemblyByName(assemblyName);

            var types = assembly.GetTypes();
            var list = types.Where(u => u.IsClass && !u.IsAbstract && !u.IsGenericType).ToList();

            foreach (var type in list)
            {
                var interfaceList = type.GetInterfaces();
                if (interfaceList.Any())
                {
                    var inter = interfaceList.Last();

                    switch (serviceLifetime)
                    {
                        case ServiceLifetime.Transient:
                            service.AddTransient(inter, type);
                            break;
                        case ServiceLifetime.Scoped:
                            service.AddScoped(inter, type);
                            break;
                        case ServiceLifetime.Singleton:
                            service.AddSingleton(inter, type);
                            break;

                    }
                    service.AddScoped(inter, type);
                }
            }
        }

    }
}
