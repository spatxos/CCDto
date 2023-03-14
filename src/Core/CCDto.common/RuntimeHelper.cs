using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace CCDto.common
{
    public class RuntimeHelper
    {
        //通过程序集的名称加载程序集
        public static Assembly GetAssemblyByName(string assemblyName)
        {
            return AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(assemblyName));
        }
        public static Assembly LoadAssembly(string assemblyName)
        {
            Assembly entry = Assembly.GetEntryAssembly();
            string dir = Path.GetDirectoryName(entry.Location);
            var filePath = Path.Combine(dir, $"{assemblyName}.dll");
            return AssemblyLoadContext.Default.LoadFromAssemblyPath(filePath);
        }

        public static List<Assembly> GetAllAssembly()
        {
            var rv = new List<Assembly>();
            var path = Assembly.GetEntryAssembly().Location;
            var dir = new DirectoryInfo(Path.GetDirectoryName(path));

            var dlls = dir.GetFiles("*.dll", SearchOption.AllDirectories);
            string[] systemdll = new string[]
            {
                "Microsoft.",
                "System.",
                "Swashbuckle.",
                "ICSharpCode",
                "Newtonsoft.",
                "Oracle.",
                "Pomelo.",
                "SQLitePCLRaw."
            };

            foreach (var dll in dlls)
            {
                try
                {
                    if (systemdll.Any(x => dll.Name.StartsWith(x)) == false)
                    {
                        rv.Add(AssemblyLoadContext.Default.LoadFromAssemblyPath(dll.FullName));
                    }
                }
                catch { }
            }
            return rv;
        }
    }
}
