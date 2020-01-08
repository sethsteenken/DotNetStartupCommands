using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DotNetStartupCommands.BrowserLauncher
{
    /// <summary>
    /// Represents separate program for internet browsing. 
    /// Implementations include the standard default browsers: Google Chrome, Firefox, MS Edge, IE, and Safari.
    /// </summary>
    public abstract class Browser
    {
        protected Browser(string name, params string[] identifiers)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            if (identifiers == null || identifiers.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(identifiers));

            Name = name;
            Identifiers = identifiers;
        }

        /// <summary>
        /// Friendly name for the internet browser application.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// List of string-based identifiers for the browser for lookup from the command line argument.
        /// </summary>
        public string[] Identifiers { get; }

        /// <summary>
        /// Unique command/agruments required when launching the browser application.
        /// </summary>
        /// <param name="url">The url for the browser to open. This will already be null-checked.</param>
        /// <returns></returns>
        protected abstract string GetLaunchCommand(string url);

        /// <summary>
        /// Build required information needed to launch the browser applications process.
        /// Will utilize <see cref="Browser.GetLaunchCommand(string)"/> for the command
        /// and will lookup the OS via <see cref="RuntimeInformation.IsOSPlatform(OSPlatform)"/> to determine launch syntax.
        /// </summary>
        /// <param name="url">Required url for the browser to open.</param>
        /// <returns>Process startup info for launching browser application.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public ProcessStartInfo GetLaunchProcess(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            var command = GetLaunchCommand(url);

            if (string.IsNullOrWhiteSpace(command))
                throw new InvalidOperationException("Browser launch command empty or null.");

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return new ProcessStartInfo("cmd", $"/c start {command}");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return new ProcessStartInfo("xdg-open", command);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return new ProcessStartInfo("open", command);
            }

            throw new InvalidOperationException("OS not found to launch browser.");
        }
    }
}
