namespace DotNetStartupCommands.BrowserLauncher
{
    public sealed class MozillaFirefoxBrowser : Browser
    {
        public override string Name => "Mozilla Firefox";

        protected override string GetLaunchCommand(string url)
        {
            return $"firefox {url}";
        }
    }
}
