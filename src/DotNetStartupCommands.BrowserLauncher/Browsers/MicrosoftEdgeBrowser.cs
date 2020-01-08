namespace DotNetStartupCommands.BrowserLauncher
{
    public sealed class MicrosoftEdgeBrowser : Browser
    {
        public MicrosoftEdgeBrowser() 
            : base("Microsoft Edge", 
                  "msedge", "edge", "e", "ms-edge", "microsoftedge", "microsoft-edge", "microsoft-edge:")
        {
        }

        protected override string GetLaunchCommand(string url)
        {
            return $"microsoft-edge:{url}";
        }
    }
}
