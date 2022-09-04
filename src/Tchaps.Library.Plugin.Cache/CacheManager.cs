using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Threading;
using Tchaps.Library.Plugin.Cache.Models;

namespace Tchaps.Plugins.Cache
{
    public class CacheManager : ICacheManager
    {
        private readonly IMemoryCache _cache;
        private readonly ICacheSetting _cacheSetting;
        private CancellationTokenSource _resetCacheToken = new CancellationTokenSource();

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheManager"/> class.
        /// </summary>
        /// <param name="memoryCache">The memory cache.</param>
        /// <param name="cacheSetting">The cache setting.</param>
        public CacheManager(IMemoryCache memoryCache, ICacheSetting cacheSetting)
        {
            _cache = memoryCache;
            _cacheSetting = cacheSetting;
            CacheDuration = _cacheSetting.Duration;
            CachingEnabled = _cacheSetting.Enabled;
        }

        /// <summary>
        /// Defaut duration in Minutes set in Config file.
        /// Return 30 seconds if not Setted.
        /// </summary>
        public int CacheDuration { get; } = 30;

        /// <summary>
        /// Gets a value indicating whether [caching enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [caching enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool CachingEnabled { get; } = true;

        /// <summary>
        /// Set Object In Memory Cache for the defined duration
        /// </summary>
        /// <param name="key">cache key</param>
        /// <param name="value">value to set for key</param>
        /// <param name="duration">cThe cache duration</param>
        /// <param name="priority"></param>
        public void SetInMemoryCache(string key, object value, int duration, CacheItemPriority priority = CacheItemPriority.Normal)
        {
            duration = duration == default ? this.CacheDuration : duration;
            DateTime now = DateTime.Now;
            // Set cache options.
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                                    // Pin to cache.
                                    .SetPriority(priority)
                                    // Keep in cache for this time, reset time if accessed.
                                    .SetSlidingExpiration(TimeSpan.FromSeconds(duration))
                                    // This should help us clear all cached data
                                    .AddExpirationToken(new CancellationChangeToken(_resetCacheToken.Token));
            _cache.Set(key, value, cacheEntryOptions);
        }

        /// <summary>
        /// Has in Cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool HasInCache(string key)
        {
            return (CachingEnabled && _cache.Get(key) != null) ? true : false;
        }

        /// <summary>
        /// Get From Cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetFromCache(string key)
        {
            return _cache.Get(key);
        }

        /// <summary>
        /// Try Get Object From Cache: Check that the object exist before retriving
        /// </summary>
        /// <param name="caching_key"></param>
        /// <returns></returns>
        public object TryGetObjectFromCache(string caching_key)
        {
            return (CachingEnabled && HasInCache(caching_key)) ? GetFromCache(caching_key) : null;
        }

        /// <summary>
        /// Try Get Object From Cache with Specific type: Check that the object exist before retriving
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="caching_key"></param>
        /// <returns></returns>
        public T TryGetFromCache<T>(string caching_key)
        {
            T result = default(T);
            if (CachingEnabled && HasInCache(caching_key))
            {
                result = (T)GetFromCache(caching_key);
            }
            return result;
        }

        /// <summary>
        /// Tries to get the object from cache. 
        /// If cache empty or not exist, call the fallBackMethod and set the result in cache if not null
        /// </summary>
        /// <typeparam name="T">Object Type</typeparam>
        /// <param name="caching_key">The caching key.</param>
        /// <param name="fallBackMethod">The fall back method.</param>
        /// <param name="cacheDuration">Duration of the cache.</param>
        /// <returns>T object</returns>
        public T TryGetFromCache<T>(string caching_key, Func<T> fallBackMethod, int cacheDuration  = default)
        {
            T result = TryGetFromCache<T>(caching_key);
            if (result != null)
                return result;
             result = fallBackMethod();
            if (result != null)
                SetInMemoryCache(caching_key, result, cacheDuration);
            return result;
        }

        /// <summary>
        /// Tries to get the object from cache. 
        /// If cache empty or not exist, call the fallBackMethod and set the result in cache if not null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="caching_key">The caching key.</param>
        /// <param name="fallBackMethod">The fall back method.</param>
        /// <param name="param">The paramter.</param>
        /// <param name="cacheDuration">Duration of the cache.</param>
        /// <returns></returns>
        public TResult TryGetFromCache<T, TResult>(string caching_key, Func<T, TResult> fallBackMethod, T param, int cacheDuration  = default)
        {
            TResult result = TryGetFromCache<TResult>(caching_key);
            if (result != null)
                return result;
            result = fallBackMethod(param);
            if (result != null)
                SetInMemoryCache(caching_key, result, cacheDuration);
            return result;
        }

        /// <summary>
        /// Tries to get the object from cache. 
        /// If cache empty or not exist, call the fallBackMethod and set the result in cache if not null
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="caching_key">The caching key.</param>
        /// <param name="fallBackMethod">The fall back method.</param>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="cacheDuration">Duration of the cache.</param>
        /// <returns></returns>
        public TResult TryGetFromCache<T1, T2, TResult>(string caching_key, Func<T1, T2, TResult> fallBackMethod, T1 param1, T2 param2, int cacheDuration = default)
        {
            TResult result = TryGetFromCache<TResult>(caching_key);
            if (result != null)
                return result;
            result = fallBackMethod(param1, param2);
            if (result != null)
                SetInMemoryCache(caching_key, result, cacheDuration);
            return result;
        }

        /// <summary>
        /// Tries to get the object from cache. 
        /// If cache empty or not exist, call the fallBackMethod and set the result in cache if not null
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="caching_key">The caching key.</param>
        /// <param name="fallBackMethod">The fall back method.</param>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        /// <param name="cacheDuration">Duration of the cache.</param>
        /// <returns></returns>
        public TResult TryGetFromCache<T1, T2, T3, TResult>(string caching_key, Func<T1, T2, T3, TResult> fallBackMethod, T1 param1, T2 param2, T3 param3, int cacheDuration = default)
        {
            TResult result = TryGetFromCache<TResult>(caching_key);
            if (result != null)
                return result;
            result = fallBackMethod(param1, param2, param3);
            if (result != null)
                SetInMemoryCache(caching_key, result, cacheDuration);
            return result;
        }

        /// <summary>
        /// Tries to get the object from cache. 
        /// If cache empty or not exist, call the fallBackMethod and set the result in cache if not null
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="T4">The type of the 4.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="caching_key">The caching key.</param>
        /// <param name="fallBackMethod">The fall back method.</param>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        /// <param name="param4">The param4.</param>
        /// <param name="cacheDuration">Duration of the cache.</param>
        /// <returns></returns>
        public TResult TryGetFromCache<T1, T2, T3, T4, TResult>(string caching_key, Func<T1, T2, T3, T4, TResult> fallBackMethod, T1 param1, T2 param2, T3 param3, T4 param4, int cacheDuration = default)
        {
            TResult result = TryGetFromCache<TResult>(caching_key);
            if (result != null)
                return result;
            result = fallBackMethod(param1, param2, param3, param4);
            if (result != null)
                SetInMemoryCache(caching_key, result, cacheDuration);
            return result;
        }

        /// <summary>
        /// Tries to get the object from cache. 
        /// If cache empty or not exist, call the fallBackMethod and set the result in cache if not null
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="T4">The type of the 4.</typeparam>
        /// <typeparam name="T5">The type of the 5.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="caching_key">The caching key.</param>
        /// <param name="fallBackMethod">The fall back method.</param>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        /// <param name="param4">The param4.</param>
        /// <param name="param5">The param5.</param>
        /// <param name="cacheDuration">Duration of the cache.</param>
        /// <returns></returns>
        public TResult TryGetFromCache<T1, T2, T3, T4, T5, TResult>(string caching_key, Func<T1, T2, T3, T4, T5, TResult> fallBackMethod, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, int cacheDuration = default)
        {
            TResult result = TryGetFromCache<TResult>(caching_key);
            if (result != null)
                return result;
            result = fallBackMethod(param1, param2, param3, param4, param5);
            if (result != null)
                SetInMemoryCache(caching_key, result, cacheDuration);
            return result;
        }

        /// <summary>
        /// Removes the object from cache.
        /// </summary>
        /// <param name="key">The key.</param>
        public void RemoveObjectFromCache(string key)
        {
            if (HasInCache(key))
            {
                _cache.Remove(key);
            }
        }

        /// <summary>
        /// Remove List Of Objects From Cache
        /// </summary>
        /// <param name="keys"></param>
        public void RemoveListOfObjectsFromCache(IEnumerable<string> keys)
        {
            foreach (string key in keys)
            {
                RemoveObjectFromCache(key);
            }
        }

        /// <summary>
        /// Clear All Cache
        /// </summary>
        public void ClearAllCache()
        {
            if (_resetCacheToken != null && !_resetCacheToken.IsCancellationRequested && _resetCacheToken.Token.CanBeCanceled)
            {
                _resetCacheToken.Cancel();
                _resetCacheToken.Dispose();
            }
            _resetCacheToken = new CancellationTokenSource();
        }

    }
}
