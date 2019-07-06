using System;
using System.Linq;

namespace DotNetStartupCommands.BrowserLauncher
{
    /// <summary>
    /// Arguments context to organized supplied commandline arguments into formatted and workable values.
    /// </summary>
    internal sealed class LaunchArgumentContext
    {
        /// <summary>
        /// Create new launch argument instance.
        /// </summary>
        /// <param name="args">Command line arguments. Must contain <see cref="Commands.Launch"/>.</param>
        /// <param name="url">Required url for the browser to open.</param>
        public LaunchArgumentContext(string[] args, string url)
        {
            if (args == null || args.Length == 0)
                throw new ArgumentNullException(nameof(args));

            var argList = args.Select(a => a.ToLower()).ToList();
            if (argList.Contains(Commands.Launch))
            {
                Valid = true;

                var launchIndex = argList.IndexOf(Commands.Launch);

                if (argList.Count > 1 && launchIndex != argList.Count - 1)
                {
                    var launchArgsValue = argList[launchIndex + 1];
                    launchArgsValue = launchArgsValue.Replace(";", ",")
                                                     .Replace("|", ",")
                                                     .ToLower()
                                                     .Trim();

                    if (launchArgsValue.Contains(","))
                    {
                        var lauchArgs = launchArgsValue.Split(',');
                        Browser = lauchArgs[0];
                        AttachDebugger = lauchArgs[1] == "debug";

                        if (lauchArgs.Length > 2 && int.TryParse(lauchArgs[2], out int delay))
                            Delay = delay;
                    }
                    else
                    {
                        Browser = launchArgsValue;
                    }
                }            
            }

            if (!string.IsNullOrWhiteSpace(url))
                Url = url.Trim();
        }

        internal bool Valid { get; }
        public string Browser { get; } = "chrome";
        public string Url { get; } = "http://localhost:5000/";
        public int Delay { get; } = 500;
        public bool AttachDebugger { get; }
    }
}
