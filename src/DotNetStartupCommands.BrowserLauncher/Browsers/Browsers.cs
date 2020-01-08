using System.Collections.Generic;

namespace DotNetStartupCommands.BrowserLauncher
{
    public static class Browsers
    {
        static Browsers()
        {
            var browsers = new Browser[] { Chrome, IE, Edge, FireFox, Safari };
            for (int b = 0; b < browsers.Length; b++)
            {
                for (int i = 0; i < browsers[b].Identifiers.Length; i++)
                {
                    (DefaultLookups as Dictionary<string, Browser>)
                        .Add(browsers[b].Identifiers[i], browsers[b]);
                }
            }
        }

        public static readonly Browser Chrome = new GoogleChromeBrowser();
        public static readonly Browser IE = new InternetExplorerBrowser();
        public static readonly Browser Edge = new MicrosoftEdgeBrowser();
        public static readonly Browser FireFox = new MozillaFirefoxBrowser();
        public static readonly Browser Safari = new SafariBrowser();

        /// <summary>
        /// Default list of browsers: Chrome, IE, Edge, FireFox, and Safari.
        /// </summary>
        public static readonly IReadOnlyDictionary<string, Browser> DefaultLookups = new Dictionary<string, Browser>();
    }
}
