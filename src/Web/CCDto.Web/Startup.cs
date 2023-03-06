using CCDto.common.AutoMapper;
using CCDto.common.Extensions;
using CCDto.Web.Core.Extensions;
using EasyNetQ;
using FreeSql;
using FreeSql.Internal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CCDto.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            // 将内置的日志组件设置为 NHibernate 的日志组件
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)//增加环境配置文件，新建项目默认有
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            //var type = Configuration["DbType"];

            //Fsql = new FreeSql.FreeSqlBuilder()
            //       .UseConnectionString(GetDbType(type), configuration[$"ConnectionStrings:{type}"])
            //           //.UseAutoSyncStructure(true)
            //           //自动同步实体结构到数据库
            //           .UseNameConvert(NameConvertType.ToUpper)
            //           .UseLazyLoading(true)
            //           //.UseNameConvert(FreeSql.Internal.NameConvertType.ToUpper)
            //           //开启延时加载，导航属性
            //           .UseMonitorCommand(cmd => Trace.WriteLine(cmd.CommandText))
            //           //跟踪SQL执行语句
            //           .Build();
        }

        public IConfiguration Configuration { get; }

        public IFreeSql Fsql { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddAutoMapper();

            services.AddAssembly("CCDto.application");
            services.AddAssembly("api.dbconnecion.application");
            services.AddAssembly("api.dbtable.application");
            services.AddAssembly("api.dbfield.application");


            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            {
                //忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //不使用驼峰样式的key
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //设置时间格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });

            //#region 多数据配置
            //if (Convert.ToBoolean(Configuration["DbsOpen"]))
            //{
            //    services.AddIdleBus();
            //}
            //#endregion

            services.AddMultiDB(Configuration);

            //services.AddSingleton(Fsql);

            services.AddSingleton(RabbitHutch.CreateBus(Configuration["MQ:Dev"]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();

            app.UseStaticHttpContext();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseSubscribe("DbSaveServicer", Assembly.GetExecutingAssembly());

            app.UseRouting();

            app.UseAutoMapper();

            app.UseStateAutoMapper();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "areas", "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });
        }
        public DataType GetDbType(string type)
        {
            switch (type.ToLower())
            {
                case "sqlserver": return DataType.SqlServer;
                case "mysql": return DataType.MySql;
                case "oracle": return DataType.Oracle;
                case "sqlite": return DataType.Sqlite;
                case "postgresql": return DataType.PostgreSQL;
                default: return DataType.SqlServer;
            }
        }
    }
}
