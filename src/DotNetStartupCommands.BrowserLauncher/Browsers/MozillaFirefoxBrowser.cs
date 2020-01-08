namespace DotNetStartupCommands.BrowserLauncher
{
    public sealed class MozillaFirefoxBrowser : Browser
    {
        public MozillaFirefoxBrowser() 
            : base("Mozilla Firefox", 
                  "firefox", "ff", "f", "fire-fox")
        {
        }

        protected override string GetLaunchCommand(string url)
        {
            return $"firefox {url}";
        }
    }
}
