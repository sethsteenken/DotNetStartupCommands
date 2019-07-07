namespace DotNetStartupCommands.BrowserLauncher
{
    public sealed class GoogleChromeBrowser : Browser
    {
        public override string Name => "Google Chrome";

        protected override string GetLaunchCommand(string url)
        {
            return $"chrome {url}";
        }
    }
}
