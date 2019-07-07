using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace DotNetStartupCommands.BrowserLauncher
{
    public static class WebHostExtensions
    {
        /// <summary>
        /// Attempt to launch browser window based on supplied command line arguments.
        /// Intended for launching browser upon a "dotnet run" command.
        /// Argument syntax: dotnet run --launch-browser [browser],debug,500
        /// Browser value defaults to chrome, debug defaults to disabled (omitted), and delay defaults to 500 milliseconds (omitted).
        /// Call this prior to <see cref="IWebHost.Start"/> or Run().
        /// Recommend only be called from a local or development-only environment.
        /// </summary>
        /// <param name="host">Web host instance.</param>
        /// <param name="args">Command line agruments. Requires <see cref="Commands.Launch"/> to launch browser.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void LaunchBrowser(this IWebHost host, string[] args)
        {
            if (host == null)
                throw new ArgumentNullException(nameof(host));

            // check in case it was registered
            var browserLauncher = host.Services.GetService<BrowserLauncher>();

            if (browserLauncher == null)
            {
                var loggerFactory = host.Services.GetRequiredService<ILoggerFactory>();

                browserLauncher = new BrowserLauncher(
                                            loggerFactory.CreateLogger<BrowserLauncher>(),
                                            Browser.DefaultLookups);
            }

            browserLauncher.Launch(args, GetLaunchUrl(host));
        }

        /// <summary>
        /// Gets registered url address from host <see cref="IWebHost.ServerFeatures"/>.
        /// Uses first value in <see cref="IServerAddressesFeature"/>.
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        internal static string GetLaunchUrl(this IWebHost host)
        {
            if (host == null)
                throw new ArgumentNullException(nameof(host));

            string url = null;
            var addressesFeature = host.ServerFeatures.Get<IServerAddressesFeature>();
            if (addressesFeature != null
                && addressesFeature.Addresses != null
                && addressesFeature.Addresses.Count > 0)
            {
                url = addressesFeature.Addresses.FirstOrDefault();
            }

            return url;
        }
    }
}
