using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace DotNetStartupCommands.BrowserLauncher
{
    public sealed class BrowserLauncher
    {
        private readonly ILogger<BrowserLauncher> _logger;
        private readonly IDictionary<string, Browser> _browserLookup;

        public BrowserLauncher(
            ILogger<BrowserLauncher> logger,
            IDictionary<string, Browser> browserLookup)
        {
            _logger = logger;
            _browserLookup = browserLookup;
        }

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
                Debugger.Launch();
        }
    }
}
