using CCDto.common.NetCore.Extensions;
using CCDto.common.NetCore.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Panda.DynamicWebApi;
using System;
using System.IO;
using CCDto.common;
using System.Reflection;

namespace Api.Template
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

            args = Environment.GetCommandLineArgs();
            double[] data = new double[100_000];
            if (args.Length > 1)
            {
                AssemblyName = args[1];
                Console.WriteLine($"获取到:{AssemblyName}");
            }

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public string[] args { get; }

        public string AssemblyName { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAssembly(AssemblyName);

            //services.AddSwaggerGen(options =>
            //{
            //    options.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });

            //    options.DocInclusionPredicate((docName, description) => true);

            //    //为 Swagger JSON and UI设置xml文档注释路径
            //    var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
            //    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

            //    //Swagger文档注释  多项目注释引入
            //    var ctlXml = Path.Combine(basePath, xmlFile);
            //    options.IncludeXmlComments(ctlXml, true);
            //    var xmlDocNames = new string[] { "ModelLibrary.xml", "dbconnecion.application.xml", "Api.Template.xml" };
            //    foreach (var xmlDocName in xmlDocNames)
            //    {
            //        var xml = Path.Combine(basePath, xmlDocName);
            //        if (File.Exists(xml))
            //            options.IncludeXmlComments(xml);
            //    }
            //});

            services.AddMultiDB(Configuration);
            services.AddMvcCore(option =>
            {
                option.EnableEndpointRouting = false;
            }).AddApiExplorer();
            //services.AddControllers();

            // 添加动态WebApi 需放在 AddMvc 之后
            //services.AddDynamicWebApi();

            //services.AddEndpointsApiExplorer();

            services.AddDynamicWebApiCore<ServiceLocalSelectController, ServiceActionRouteFactory>();

            //services.TryAddSingleton<IApiDescriptionGroupCollectionProvider, ApiDescriptionGroupCollectionProvider>();
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseDynamicWebApi((serviceProvider, options) =>
            {
                if (!string.IsNullOrWhiteSpace(AssemblyName))
                {
                    options.AddAssemblyOptions(RuntimeHelper.LoadAssembly(AssemblyName));
                }
            });

            app.UseExceptionMiddleware();
            app.UseStaticFiles();

            //app.UseSwagger();
            //app.UseSwaggerUI(options =>
            //{
            //    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            //    options.RoutePrefix = string.Empty;
            //});

            app.UseRouting();

            app.UseMvc();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/time", async context =>
                {
                    await context.Response.WriteAsync($"{DateTime.Now:HH:mm:ss}");
                });
            });


            //app.Run(context =>
            //{
            //    return context.Response.WriteAsync("Hello world");
            //});
        }
    }
}
