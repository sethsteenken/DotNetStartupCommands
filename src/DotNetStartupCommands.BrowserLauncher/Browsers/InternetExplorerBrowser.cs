namespace DotNetStartupCommands.BrowserLauncher
{
    public sealed class InternetExplorerBrowser : Browser
    {
        public InternetExplorerBrowser() 
            : base("Internet Explorer", 
                  "ie", "internet-explorer", "iexplore", "iexplorer", "explorer", "internetexplorer")
        {
        }

        protected override string GetLaunchCommand(string url)
        {
            return $"iexplore {url}";
        }         
    }
}
