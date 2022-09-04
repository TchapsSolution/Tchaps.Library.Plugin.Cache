# Docs
Tchaps.Library.Plugin.Cache is a simple helper that use In-Memory Cache for .net core application.

## install
install the helper library through the command.
- Package Manager
> Install-Package Tchaps.Library.Plugin.Cache
- .NET CLI
> dotnet add package Tchaps.Library.Plugin.Cache

## Add Cache setting in the appsettings.json
The cache setting has 2 parameters:
- Enabled : a boolean to enable or disable the cache

The default value for Enabled is : True

- Duration: The time in second that the object should be hold in the cache
The default value for Duration is : 30s

>       "CacheSetting": {
>            "Enabled": "True",
>            "Duration": 30
>        }

## Configure the startup service:

### 1- Add Configuration property and inject it through Dependency injection

>  public IConfiguration Configuration { get; }
 
 ### 2- Add service in ConfigureServices

>       public void ConfigureServices(IServiceCollection services)
>       {
>           // .... 
>           services.AddTchapsLibraryPluginCache()
>           //
>       }

## How to use in controller or service 
Inject the ICacheManager in the controller or the service 
>       public class HomeController
>       {
>           protected readonly ICacheManager _cacheManager;
>           public HomeController(ICacheManager cacheManager)
>           {
>               _cacheManager = cacheManager;
>           }
>       }




