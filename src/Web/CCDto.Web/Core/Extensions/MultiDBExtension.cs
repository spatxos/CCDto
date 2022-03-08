using CCDto.common.FreeSql;
using CCDto.entity.FreeSql;
using FreeSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCDto.common.Extensions
{
    public static class MultiDBExtension
    {
        public static IServiceCollection AddMultiDB(this IServiceCollection services, IConfiguration Configuration)
        {
            var fsql = new MultiFreeSql();
            MultiDb[] dbsList = Configuration.GetSection("Dbs").Get<MultiDb[]>();
            if (dbsList.Any())
            {
                foreach (var dbObj in dbsList)
                {
                    fsql.Register(dbObj.Name, () => new FreeSqlBuilder().UseConnectionString(dbObj.DbType, dbObj.ConnectionString).Build());
                }
            }
            services.AddSingleton<IFreeSql>(fsql);

            return services;
        }
    }
}
