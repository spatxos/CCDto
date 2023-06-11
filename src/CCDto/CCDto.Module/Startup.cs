
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using CCDto.Common;

namespace CCDto.Module
{
    public class Startup
    {
        public string[] GetArgs()
        {
            return Environment.GetCommandLineArgs();
        }
        public virtual void Register(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            // 将内置的日志组件设置为 NHibernate 的日志组件
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)//增加环境配置文件，新建项目默认有
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; set; }

        public virtual void ConfigureServices(IServiceCollection services)
        {

            services.AddHttpClient();

            //services.AddAssembly(AssemblyName);

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
            //    var xmlDocNames = new string[] { "ModelLibrary.xml", "dbconnecion.application.xml", "CCDto.Module.xml" };
            //    foreach (var xmlDocName in xmlDocNames)
            //    {
            //        var xml = Path.Combine(basePath, xmlDocName);
            //        if (File.Exists(xml))
            //            options.IncludeXmlComments(xml);
            //    }
            //});

            //services.AddMultiDB(Configuration);
            services.AddMvcCore(option =>
            {
                option.EnableEndpointRouting = false;
            }).AddApiExplorer();
            services.AddControllers();

        }
        public virtual void Configure(IApplicationBuilder app)
        {
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
