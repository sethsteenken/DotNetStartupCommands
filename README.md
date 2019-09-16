# DotNetStartupCommands
Extension to the dotnet CLI Commands.

## BrowserLauncher
Run command when launching AspNetCore applications to allow for opening browser, debugging, etc. Compliments the * *dotnet run* * command.
> --launch-browser

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

Argument * *--launch-browser* * is required for the LaunchBrowser functionality to be enabled. Passing no further arguments will utilize default values:

 - Browser: chrome
 - Debug: false
 - Delay: 500 milliseconds

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
dotnet run --launch-browser safari,nodebug,2000
```
### Browsers
Here's the list of launchable browsers and associated commands recognized by each browser:

**Google Chrome** (default)
- chrome
- googlechrome
- google-chrome
- c

**Mozilla Firefox**
- firefox
- fire-fox
- ff
- f

**Microsoft Edge**
- msedge
- ms-edge
- edge
- microsoftedge
- microsoft-edge
- microsoft-edge:
- e

**Internet Explorer**
- internetexplorer
- internet-explorer
- iexplore
- iexplorer
- explorer
- ie

**Safari**
- safari
- saf
- s

## Contributing

If you'd like to contribute, please fork the repository and use a feature
branch. Pull requests are more than welcome!

## Links

- Project / Repository: https://github.com/sethsteenken/DotNetStartupCommands.BrowserLauncher
- Issue tracker: https://github.com/sethsteenken/DotNetStartupCommands.BrowserLauncher/issues
- Related projects:
  - Bunder: https://github.com/sethsteenken/Bunder
  - gulp-bunder: https://github.com/sethsteenken/gulp-bunder
  - Integreat: https://github.com/sethsteenken/Integreat

## Licensing

The code in this project is licensed under MIT license. [View the license here](LICENSE.md)

