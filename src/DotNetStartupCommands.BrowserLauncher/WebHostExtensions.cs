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

            string url = null;
            var addressesFeature = host.ServerFeatures.Get<IServerAddressesFeature>();
            if (addressesFeature != null 
                && addressesFeature.Addresses != null 
                && addressesFeature.Addresses.Count > 0)
            {
                url = addressesFeature.Addresses.FirstOrDefault();
            }

            browserLauncher.Launch(args, url);
        }
    }
}
