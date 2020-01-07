using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace DotNetStartupCommands.BrowserLauncher
{
    internal static class Launcher
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="args"></param>
        /// <param name="url"></param>
        public static void Launch(IServiceProvider serviceProvider, string[] args, string url)
        {
            if (serviceProvider == null)
                throw new ArgumentNullException(nameof(serviceProvider));
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            // check in case it was registered
            var browserLauncher = serviceProvider.GetService<BrowserLauncher>();

            if (browserLauncher == null)
            {
                var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

                browserLauncher = new BrowserLauncher(
                                            loggerFactory.CreateLogger<BrowserLauncher>(),
                                            Browser.DefaultLookups);
            }

            browserLauncher.Launch(args, url);
        }
    }
}
