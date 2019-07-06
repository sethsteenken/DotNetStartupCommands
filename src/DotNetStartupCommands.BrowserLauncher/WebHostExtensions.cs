using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace DotNetStartupCommands.BrowserLauncher
{
    public static class WebHostExtensions
    {
        public static void LaunchBrowser(this IWebHost host, string[] args)
        {
            if (host == null)
                throw new ArgumentNullException(nameof(host));

            var browserLauncher = host.Services.GetService<BrowserLauncher>();

            if (browserLauncher == null)
            {
                var loggerFactory = host.Services.GetRequiredService<ILoggerFactory>();

                browserLauncher = new BrowserLauncher(
                                            loggerFactory.CreateLogger<BrowserLauncher>(),
                                            Browser.DefaultLookups);
            }

            browserLauncher.Launch(args);
        }
    }
}
