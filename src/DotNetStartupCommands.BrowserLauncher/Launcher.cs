using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace DotNetStartupCommands.BrowserLauncher
{
    internal static class Launcher
    {
        /// <summary>
        /// Establish <see cref="BrowserLauncher"/> and launch browser application 
        /// based on <paramref name="args"/> value and under url at <paramref name="url"/>.
        /// </summary>
        /// <param name="serviceProvider">Establised service provider. If <see cref="BrowserLauncher"/> has been registered, it will be used to launch browser application.</param>
        /// <param name="args">Arguments for launching browser application.</param>
        /// <param name="url">Optional url on which the application is listening. The default is used if empty.</param>
        public static void Launch(IServiceProvider serviceProvider, string[] args, string url)
        {
            if (serviceProvider == null)
                throw new ArgumentNullException(nameof(serviceProvider));

            // check in case it was registered
            var browserLauncher = serviceProvider.GetService<BrowserLauncher>();

            if (browserLauncher == null)
            {
                var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

                // allow for custom lookups
                var lookups = serviceProvider.GetService<IReadOnlyDictionary<string, Browser>>();
                if (lookups == null)
                    lookups = Browsers.DefaultLookups;

                browserLauncher = new BrowserLauncher(
                                            loggerFactory.CreateLogger<BrowserLauncher>(),
                                            lookups);
            }

            browserLauncher.Launch(args, url);
        }
    }
}
