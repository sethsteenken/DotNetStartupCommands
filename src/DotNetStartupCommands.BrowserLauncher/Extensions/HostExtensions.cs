using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace DotNetStartupCommands.BrowserLauncher
{
    public static class HostExtensions
    {
        /// <summary>
        /// Attempt to launch browser window based on supplied command line arguments.
        /// Intended for launching browser upon a "dotnet run" command.
        /// Argument syntax: dotnet run --launch-browser [browser],debug,500
        /// Browser value defaults to chrome, debug defaults to disabled (omitted), and delay defaults to 500 milliseconds (omitted).
        /// Call this prior to <see cref="IHost.StartAsync(System.Threading.CancellationToken)"/> or Run().
        /// Recommend only be called from a local or development-only environment.
        /// </summary>
        /// <param name="host">Generic host instance that contains <see cref="IServer"></see>.</param>
        /// <param name="args">Command line agruments. Requires <see cref="Commands.Launch"/> to launch browser.</param>
        public static void LaunchBrowser(this IHost host, string[] args)
        {
            if (host == null)
                throw new ArgumentNullException(nameof(host));

            Launcher.Launch(host.Services, args, GetLaunchUrl(host));
        }

        /// <summary>
        /// Gets registered url address from host <see cref="IHost.Services"/>.
        /// Uses first value in <see cref="IServerAddressesFeature"/> from either the service collection or from <see cref="IServer"/>.
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        internal static string GetLaunchUrl(this IHost host)
        {
            if (host == null)
                throw new ArgumentNullException(nameof(host));

            var addressesFeature = host.Services.GetService<IServerAddressesFeature>();
            if (addressesFeature == null)
            {
                var server = host.Services.GetService<IServer>();
                if (server == null)
                    return null;

                addressesFeature = server.Features.Get<IServerAddressesFeature>();
            }

            return addressesFeature?.GetLaunchUrl();
        }
    }
}
