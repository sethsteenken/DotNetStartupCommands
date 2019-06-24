using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetStartupCommands.BrowserLauncher
{
    public sealed class BrowserLauncher
    {
        private readonly ILogger<BrowserLauncher> _logger;

        public BrowserLauncher(ILogger<BrowserLauncher> logger)
        {
            _logger = logger;
        }

        public void Launch(string[] args)
        {

        }
    }
}
