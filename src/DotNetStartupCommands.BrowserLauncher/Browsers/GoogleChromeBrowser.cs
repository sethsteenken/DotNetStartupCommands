namespace DotNetStartupCommands.BrowserLauncher
{
    public sealed class GoogleChromeBrowser : Browser
    {
        public GoogleChromeBrowser() 
            : base("Google Chrome", 
                  "chrome", "c", "googlechrome", "google-chrome")
        {

        }

        protected override string GetLaunchCommand(string url)
        {
            return $"chrome {url}";
        }
    }
}
