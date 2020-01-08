namespace DotNetStartupCommands.BrowserLauncher
{
    public sealed class SafariBrowser : Browser
    {
        public SafariBrowser() 
            : base("Safari", 
                  "safari", "s", "saf")
        {
        }

        protected override string GetLaunchCommand(string url)
        {
            return $"-a Safari {url}";
        }
    }
}
