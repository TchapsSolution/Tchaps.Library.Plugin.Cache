using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace Tchaps.Plugins.Cache
{
    /// <summary>
    /// The interface for the cache Manager
    /// </summary>
    public interface ICacheManager
    {
        /// <summary>
        /// Defaut duration in Minutes set in Config file.
        /// Return 30 seconds if not Setted.
        /// </summary>
        int CacheDuration { get; }

        /// <summary>
        /// Gets a value indicating whether [caching enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [caching enabled]; otherwise, <c>false</c>.
        /// </value>
        bool CachingEnabled { get; }

        /// <summary>
        /// Set Object In Memory Cache for the defined duration
        /// </summary>
        /// <param name="key">cache key</param>
        /// <param name="value">value to set for key</param>
        /// <param name="duration">cThe cache duration</param>
        /// <param name="priority"></param>
        void SetInMemoryCache(string key, object value, int duration, CacheItemPriority priority = CacheItemPriority.Normal);

        /// <summary>
        /// Has in Cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool HasInCache(string key);

        /// <summary>
        /// Get From Cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object GetFromCache(string key);

        /// <summary>
        /// Try Get Object From Cache: Check that the object exist before retriving
        /// </summary>
        /// <param name="caching_key"></param>
        /// <returns></returns>
        object TryGetObjectFromCache(string caching_key);

        /// <summary>
        /// Try Get Object From Cache with Specific type: Check that the object exist before retriving
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="caching_key"></param>
        /// <returns></returns>
        T TryGetFromCache<T>(string caching_key);

        /// <summary>
        /// Tries to get the object from cache. 
        /// If cache empty or not exist, call the fallBackMethod and set the result in cache if not null
        /// </summary>
        /// <typeparam name="T">Object Type</typeparam>
        /// <param name="caching_key">The caching key.</param>
        /// <param name="fallBackMethod">The fall back method.</param>
        /// <param name="cacheDuration">Duration of the cache.</param>
        /// <returns>T object</returns>
        T TryGetFromCache<T>(string caching_key, Func<T> fallBackMethod, int cacheDuration = default);

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
        TResult TryGetFromCache<T, TResult>(string caching_key, Func<T, TResult> fallBackMethod, T param, int cacheDuration = default);

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
        TResult TryGetFromCache<T1, T2, TResult>(string caching_key, Func<T1, T2, TResult> fallBackMethod, T1 param1, T2 param2, int cacheDuration = default);

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
        TResult TryGetFromCache<T1, T2, T3, TResult>(string caching_key, Func<T1, T2, T3, TResult> fallBackMethod, T1 param1, T2 param2, T3 param3, int cacheDuration = default);

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
        TResult TryGetFromCache<T1, T2, T3, T4, TResult>(string caching_key, Func<T1, T2, T3, T4, TResult> fallBackMethod, T1 param1, T2 param2, T3 param3, T4 param4, int cacheDuration = default);

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
        TResult TryGetFromCache<T1, T2, T3, T4, T5, TResult>(string caching_key, Func<T1, T2, T3, T4, T5, TResult> fallBackMethod, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, int cacheDuration = default);

        /// <summary>
        /// Removes the object from cache.
        /// </summary>
        /// <param name="key">The key.</param>
        void RemoveObjectFromCache(string key);

        /// <summary>
        /// Remove List Of Objects From Cache
        /// </summary>
        /// <param name="keys"></param>
        void RemoveListOfObjectsFromCache(IEnumerable<string> keys);

        /// <summary>
        /// Clear All Cache
        /// </summary>
        void ClearAllCache();
    }
}
