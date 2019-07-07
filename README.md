# DotNetStartupCommands
Extension to the dotnet cli Commands.

## BrowserLauncher
Run command when launching AspNetCore applications to allow for opening browser, debugging, etc. Compliments the dotnet run command.

### Installing and Setup
BrowserLauncher is available as a [Nuget Package](https://www.nuget.org/packages/DotNetStartupCommands.BrowserLauncher). Add to AspNetCore-based project via Nuget Package Manager in Visual Studio or via Package Manager Console.

```shell
PM> Install-Package DotNetStartupCommands.BrowserLauncher
```

### Setup
Inside Program.cs in an AspNetCore project, call LaunchBrowser extension method on IWebHost:

```csharp
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

#if DEBUG
            // Opens browser upon run if command was supplied in arguments.
            host.LaunchBrowser(args);
#endif

            host.Run();
        }
```

### Use
When starting an AspNetCore application via the dotnet CLI, pass arguments to target BrowserLauncher and launch a browser with optional debugging.

Argument --launch-browser is required for the LaunchBrowser functionality to be enabled. Passing no further arguments will utilize default values:

Browser: chrome
Debug: false
Delay: 500 milliseconds

This default command will start an AspNetCore application and then launch Google Chrome without attaching a debugger and will delay 500 milliseconds before opening browser:
```powershell
dotnet run --launch-browser
```

This command will start an AspNetCore application and then launch FireFox:
```powershell
dotnet run --launch-browser firefox
```

This command will start an AspNetCore application, kick off the debugger attaching process, and open Microsoft Edge. The "debug" argument must be "debug" to trigger the debugging process
```powershell
dotnet run --launch-browser msedge,debug
```

This command will start an AspNetCore application and open Safari after a 2 second delay:
```powershell
dotnet run --launch-browser saf,nodebug,2000
```powershell

