using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace remind_me_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((webHostBuilderContext, configurationbuilder) =>
            {
                var env = webHostBuilderContext.HostingEnvironment;
                configurationbuilder.SetBasePath(env.ContentRootPath);
                configurationbuilder.AddJsonFile("appsettings.json", false, true);
                configurationbuilder.AddJsonFile($"appsettings.{env}.json", true, true);
                configurationbuilder.AddJsonFile($"secrets/appsettings.secrets.json", true, true); // we will override the settings saved here.
                configurationbuilder.AddEnvironmentVariables();
            })
            .UseStartup<Startup>()
            .Build();
    }
}