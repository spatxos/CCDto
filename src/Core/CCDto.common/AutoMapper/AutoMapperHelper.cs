using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CCDto.common.AutoMapper
{
    public static class AutoMapperHelper
    {
        private static IServiceProvider ServiceProvider;

        public static void UseStateAutoMapper(this IApplicationBuilder applicationBuilder)
        {
            ServiceProvider = applicationBuilder.ApplicationServices;
        }
        public static void AddStateAutoMapper(this IServiceProvider service)
        {
            ServiceProvider = service;
        }

        public static TDestination Map<TDestination>(object source)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();

            return mapper.Map<TDestination>(source);
        }

        public static TDestination Map<TSource, TDestination>(TSource source)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();

            return mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();

            return mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TDestination>(this object source)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();

            return mapper.Map<TDestination>(source);
        }

        public static List<TDestination> MapToList<TDestination>(this IEnumerable source)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();

            return mapper.Map<List<TDestination>>(source);
        }


        public static List<TDestination> MapToList<TSource, TDestination>(this IEnumerable<TSource> source)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<List<TDestination>>(source);
        }
    }
}
