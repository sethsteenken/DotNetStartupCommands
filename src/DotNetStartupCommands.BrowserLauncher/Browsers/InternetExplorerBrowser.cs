namespace DotNetStartupCommands.BrowserLauncher
{
    internal class InternetExplorerBrowser : Browser
    {
        public override string Name => "Internet Explorer";

        protected override string GetLaunchCommand(string url)
        {
            return $"iexplore {url}";
        }
    }
}
