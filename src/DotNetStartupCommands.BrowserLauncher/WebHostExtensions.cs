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

            var loggerFactory = host.Services.GetRequiredService<ILoggerFactory>();

            var browserLauncher = new BrowserLauncher(
                                            loggerFactory.CreateLogger<BrowserLauncher>(),
                                            Browser.GetDefaultLookups());

            browserLauncher.Launch(args);
        }
    }
}
