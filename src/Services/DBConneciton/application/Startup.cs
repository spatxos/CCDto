using CCDto.Entity.Attributes;
using CCDto.Entity.Enums;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.dbconnecion.application
{
    public class Startup: CCDto.Module.Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            base.Register(configuration, env);
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "接口文档",
                    Version = "v1",
                    Description = "测试 webapi"
                });

                // 遍历ApiGroupNames所有枚举值生成接口文档，Skip(1)是因为Enum第一个FieldInfo是内置的一个Int值
                typeof(ApiGroupNames).GetFields().Skip(1).ToList().ForEach(f =>
                {
                    //获取枚举值上的特性
                    var info = f.GetCustomAttributes(typeof(GroupInfoAttribute), false).OfType<GroupInfoAttribute>().FirstOrDefault();
                    options.SwaggerDoc(f.Name, new OpenApiInfo
                    {
                        Title = info?.Title,
                        Version = info?.Version,
                        Description = info?.Description
                    });
                });
                // 没有特性的接口分到NoGroup上
                options.SwaggerDoc("NoGroup", new OpenApiInfo
                {
                    Title = "无分组"
                });
                // 判断接口归于哪个分组
                options.DocInclusionPredicate((docName, apiDescription) =>
                {
                    if (docName == "NoGroup")
                    {
                        // 当分组为NoGroup时，只要没加特性的接口都属于这个组
                        return string.IsNullOrEmpty(apiDescription.GroupName);
                    }
                    else
                    {
                        return apiDescription.GroupName == docName;
                    }
                });
            });

            //base.ConfigureServices(services);

        }

        public override void Configure(IApplicationBuilder app)
        {
            //base.Configure(app);
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                // 遍历ApiGroupNames所有枚举值生成接口文档
                typeof(ApiGroupNames).GetFields().Skip(1).ToList().ForEach(f =>
                {
                    //获取枚举值上的特性
                    var info = f.GetCustomAttributes(typeof(GroupInfoAttribute), false).OfType<GroupInfoAttribute>().FirstOrDefault();
                    options.SwaggerEndpoint($"/swagger/{f.Name}/swagger.json", info != null ? info.Title : f.Name);
                });
                options.SwaggerEndpoint("/swagger/NoGroup/swagger.json", "无分组");
                options.RoutePrefix = string.Empty;
            });
        }
    }
}
