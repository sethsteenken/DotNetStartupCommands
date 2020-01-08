using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace DotNetStartupCommands.BrowserLauncher
{
    /// <summary>
    /// Performs the browser application launching event based on command line arguments.
    /// </summary>
    public sealed class BrowserLauncher
    {
        private readonly ILogger<BrowserLauncher> _logger;
        private readonly IReadOnlyDictionary<string, Browser> _browserLookup;

        /// <summary>
        /// Creates a browser launching instance.
        /// </summary>
        /// <param name="logger">
        /// Logger for events, warnings, and errors. 
        /// Requires <see cref="ILoggerFactory"/> in default implementation.
        /// </param>
        /// <param name="browserLookup">
        /// All available browsers registered to lookup keywords.
        /// Uses <see cref="Browsers.DefaultLookups"/> by default.
        /// </param>
        public BrowserLauncher(
            ILogger<BrowserLauncher> logger,
            IReadOnlyDictionary<string, Browser> browserLookup)
        {
            _logger = logger;
            _browserLookup = browserLookup;
        }

        /// <summary>
        /// Perform browser application launch based on command line arguments and a url.
        /// </summary>
        /// <param name="args">Command line arguments. Must contain launch command <see cref="Commands.Launch"/>.</param>
        /// <param name="url">Url for browser to open. Defaults to http://localhost:5000/. </param>
        public void Launch(string[] args, string url)
        {
            try
            {
                if (args == null || args.Length == 0 || !args.Any(a => a.ToLower() == Commands.Launch))
                {
                    _logger.LogInformation("No arguments for launching browser.");
                    return;
                }

                LaunchInternal(new LaunchArgumentContext(args, url));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error launching browser. {ex.Message}", ex);
                return;
            }
        }

        private void LaunchInternal(LaunchArgumentContext context)
        {
            if (!context.Valid)
            {
                _logger.LogWarning($"Browser launching argument(s) invalid. Arguments for launching browser must start with {Commands.Launch}.");
                return;
            }

            if (!_browserLookup.TryGetValue(context.Browser, out Browser browser))
            {
                _logger.LogWarning($"Browser argument '{context.Browser}' not found as a valid browser.");
                return;
            }

            if (context.Delay > 0)
            {
                _logger.LogInformation($"Delaying thread for {context.Delay} milliseconds..."); ;
                Thread.Sleep(context.Delay);
            }

            _logger.LogInformation($"Running process to open '{context.Url}' in browser {browser.Name}...");
            Process.Start(browser.GetLaunchProcess(context.Url));

            if (context.AttachDebugger)
            {
                _logger.LogInformation("Attaching debugger...");
                Debugger.Launch();
            }
        }
    }
}
