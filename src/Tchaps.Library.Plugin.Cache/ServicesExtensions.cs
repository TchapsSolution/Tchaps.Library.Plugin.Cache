using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tchaps.Library.Plugin.Cache.Models;
using Tchaps.Plugins.Cache;

namespace Tchaps.Library.Plugin.Cache
{
    public static class ServicesExtensions
    {
        public static void AddTchapsLibraryPluginCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICacheManager, CacheManager>();
            services.AddScoped<ICacheSetting, CacheSetting>();
            var cacheSetting = configuration.GetSection($"{nameof(CacheSetting)}").Get<CacheSetting>();
            services.AddSingleton<ICacheSetting>(cacheSetting);
        }
    }
}
