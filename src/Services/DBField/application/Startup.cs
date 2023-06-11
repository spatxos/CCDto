using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.dbfield.application
{
    public class Startup: CCDto.Module.Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            base.Register(configuration, env);
        }
    }
}
