using Microsoft.AspNetCore.Hosting.Server.Features;
using System.Linq;

namespace DotNetStartupCommands.BrowserLauncher
{
    internal static class ServerAddressesFeatureExtensions
    {
        /// <summary>
        /// Returns first value in <see cref="IServerAddressesFeature.Addresses"/>.
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        public static string GetLaunchUrl(this IServerAddressesFeature feature)
        {
            if (feature == null)
                return null;

            if (feature.Addresses != null && feature.Addresses.Count > 0)
                return feature.Addresses.FirstOrDefault();

            return null;
        }
    }
}
