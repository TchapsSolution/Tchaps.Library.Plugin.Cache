using Microsoft.Extensions.Caching.Memory;
using Tchaps.Library.Plugin.Cache.Models;
using Tchaps.Plugins.Cache;

namespace Tchaps.Library.Plugin.Cache.Test
{
    public class CacheManagerTests
    {
        ICacheManager _cacheManager;
        IMemoryCache _cache;
        ICacheSetting _cacheSetting;

        [Fact]
        public void GivenCacheEnabled_ShouldSetCacheEntries()
        {
            // Arrange
            _cacheManager = GetCacheManager();
            (string key, string data) = ("Tchaps", "Solution");

            // Act
            _cacheManager.SetInMemoryCache(key, data, duration: default);

            // Assert
            Assert.True(_cacheManager.HasInCache(key));
            Assert.Equal(data, _cacheManager.TryGetObjectFromCache(key));
        }

        [Fact]
        public void GivenCacheDisabled_ShouldNotSetCacheEntries()
        {
            // Arrange
            _cacheManager = GetCacheManager(enabled: false);
            (string key, string data) = ("Tchaps", "Solution");

            // Act
            _cacheManager.SetInMemoryCache(key, data, duration: default);

            // Assert
            Assert.False(_cacheManager.HasInCache(key));
            Assert.Null(_cacheManager.TryGetObjectFromCache(key));
        }

        [Theory]
        [InlineData("key_1", "Solution")]
        [InlineData("key_2", new[] {1, 2, 3, 5})]
        public void GivingCacheObject_ShouldReturnTheObjectFromCache(string key, object value)
        {
            // Arrange
            _cacheManager = GetCacheManager();

            // Act
            _cacheManager.SetInMemoryCache(key, value, duration: default);

            // Assert
            Assert.Equal(value, _cacheManager.TryGetObjectFromCache(key));
        }


        [Theory]
        [InlineData("Company", "Tchaps Solution")]
        [InlineData("cities", new[] { "Yaounde, Montreal, Moscow, Paris, Washington" })]
        public void RemoveCacheObject_ShouldReturnNULLFromCache(string key, object value)
        {
            // Arrange
            _cacheManager = GetCacheManager();          
            _cacheManager.SetInMemoryCache(key, value, duration: default);
            Assert.True(_cacheManager.HasInCache(key));
            Assert.Equal(value, _cacheManager.TryGetObjectFromCache(key));

            // Act
            _cacheManager.RemoveObjectFromCache(key);

            // Assert
            Assert.False(_cacheManager.HasInCache(key));
            Assert.Null(_cacheManager.TryGetObjectFromCache(key));
        }


        [Theory]
        [InlineData("Company", "Tchaps Solution", 2)]
        [InlineData("cities", new[] { "Yaounde, Montreal, Moscow, Paris, Washington" }, 3)]
        public void GivenCaches_ShouldExpiredAfterDuration(string key, object value, short duration)
        {
            // Arrange
            _cacheManager = GetCacheManager();
            _cacheManager.SetInMemoryCache(key, value, duration);
            Assert.True(_cacheManager.HasInCache(key)); // Validate that data is stored in cache before expiration

            // Act
            Thread.Sleep(++duration * 1000);

            // Assert
            Assert.False(_cacheManager.HasInCache(key));
            Assert.Null(_cacheManager.TryGetObjectFromCache(key));
        }

        [Fact]
        public void ClearingCache_ShouldReturnEmptyCacheEntries()
        {
            // Arrange
            _cacheManager = GetCacheManager(enabled: true);
            var dbData = new Dictionary<string, object> {
                {"Developer", "Tchaps" },
                {"Sites", "TchapsSolution.Com, CarerBoard.Ca" },
                {"cities" , new []{"Yaounde, Montreal, Moscow" } },
            };
            foreach(var data in dbData)
            {
                _cacheManager.SetInMemoryCache(data.Key, data.Value, duration: default);
                Assert.True(_cacheManager.HasInCache(data.Key)); // Validate that data is stored in cache before clearing
            }

            // Act
            _cacheManager.ClearAllCache();

            // Assert
            foreach (var data in dbData)
                Assert.False(_cacheManager.HasInCache(data.Key));
        }


        public CacheManager GetCacheManager(short duration = 2, bool enabled = true)
        {
            _cacheSetting = new CacheSetting { Duration = duration, Enabled = enabled };
            _cache = new MemoryCache(new MemoryCacheOptions());
            var cacheManager = new CacheManager(_cache, _cacheSetting);
            return cacheManager;
        }

    }
}
