namespace DotNetStartupCommands.BrowserLauncher
{
    internal class SafariBrowser : Browser
    {
        public override string Name => "Safari";

        protected override string GetLaunchCommand(string url)
        {
            return $"-a Safari {url}";
        }
    }
}
