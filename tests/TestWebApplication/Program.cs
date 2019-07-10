using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using DotNetStartupCommands.BrowserLauncher;
using System.Diagnostics;

namespace TestWebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

#if DEBUG
            // Opens browser upon run if command was supplied in arguments.
            host.LaunchBrowser(args);
#endif

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //.UseUrls("http://localhost:3333/")
                .UseStartup<Startup>();
    }
}
