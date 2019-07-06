namespace DotNetStartupCommands.BrowserLauncher
{
    internal class MicrosoftEdgeBrowser : Browser
    {
        public override string Name => "Microsoft Edge";

        protected override string GetLaunchCommand(string url)
        {
            return $"microsoft-edge:{url}";
        }
    }
}
