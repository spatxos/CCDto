using AutoMapper;
using AutoMapper.Configuration;
using CCDto.common;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CCDto.Web.Core.Extensions
{
    public static class AutoMapperExtension
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection service)
        {

            service.TryAddSingleton<ProfileFactory>();

            service.TryAddSingleton<MapperConfigurationExpression>();

            service.TryAddSingleton(serviceProvider =>
            {
                var mapperConfigurationExpression = serviceProvider.GetRequiredService<MapperConfigurationExpression>();
                var factory = serviceProvider.GetRequiredService<ProfileFactory>();

                var instance = new MapperConfiguration(mapperConfigurationExpression);

                foreach (var profile in factory.ConvertList)
                {
                    mapperConfigurationExpression.AddProfile(profile);

                    instance = new MapperConfiguration(mapperConfigurationExpression);

                    try
                    {
                        instance.AssertConfigurationIsValid();
                    }
                    catch(Exception e)
                    {
                        var i = 0;
                    }
                }

                return instance;
            });

            service.TryAddSingleton(serviceProvider =>
            {
                var mapperConfiguration = serviceProvider.GetRequiredService<MapperConfiguration>();

                return mapperConfiguration.CreateMapper();
            });

            return service;
        }

        public static IMapperConfigurationExpression UseAutoMapper(this IApplicationBuilder applicationBuilder)
        {
            var factory = applicationBuilder.ApplicationServices.GetRequiredService<ProfileFactory>();
            factory.AddAssemblys(RuntimeHelper.GetAllAssembly().ToArray());
            return applicationBuilder.ApplicationServices.GetRequiredService<MapperConfigurationExpression>();
        }
    }
}
