namespace DotNetStartupCommands.BrowserLauncher
{
    internal class MozillaFirefoxBrowser : Browser
    {
        public override string Name => "Mozilla Firefox";

        protected override string GetLaunchCommand(string url)
        {
            return $"firefox {url}";
        }
    }
}
