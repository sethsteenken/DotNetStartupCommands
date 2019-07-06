using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DotNetStartupCommands.BrowserLauncher
{
    public abstract class Browser
    {
        private static IDictionary<string, Browser> _defaultLookups;

        public abstract string Name { get; }
        protected abstract string GetLaunchCommand(string url);

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

        public static IDictionary<string, Browser> DefaultLookups
        {
            get
            {
                if (_defaultLookups == null)
                {
                    _defaultLookups = new Dictionary<string, Browser>()
                    {
                        { "chrome", new GoogleChromeBrowser() },
                        { "c", new GoogleChromeBrowser() },
                        { "googlechrome", new GoogleChromeBrowser() },
                        { "google-chrome", new GoogleChromeBrowser() },

                        { "ie", new InternetExplorerBrowser() },
                        { "internet-explorer", new InternetExplorerBrowser() },
                        { "iexplore", new InternetExplorerBrowser() },
                        { "iexplorer", new InternetExplorerBrowser() },
                        { "explorer", new InternetExplorerBrowser() },
                        { "internetexplorer", new InternetExplorerBrowser() },

                        { "msedge", new MicrosoftEdgeBrowser() },
                        { "edge", new MicrosoftEdgeBrowser() },
                        { "e", new MicrosoftEdgeBrowser() },
                        { "ms-edge", new MicrosoftEdgeBrowser() },
                        { "microsoftedge", new MicrosoftEdgeBrowser() },
                        { "microsoft-edge", new MicrosoftEdgeBrowser() },
                        { "microsoft-edge:", new MicrosoftEdgeBrowser() },

                        { "firefox", new MozillaFirefoxBrowser() },
                        { "ff", new MozillaFirefoxBrowser() },
                        { "f", new MozillaFirefoxBrowser() },
                        { "fire-fox", new MozillaFirefoxBrowser() },

                        { "safari", new SafariBrowser() },
                        { "s", new SafariBrowser() },
                        { "saf", new SafariBrowser() }
                    };
                }

                return _defaultLookups;
            }
        }
    }
}
