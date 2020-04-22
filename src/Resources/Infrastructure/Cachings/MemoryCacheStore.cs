using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Cachings
{
    // http://www.kamilgrzybek.com/design/cache-aside-pattern-in-net-core/
    public class MemoryCacheStore : ICacheStore
    {
        private readonly IMemoryCache _memoryCache;
        private readonly Dictionary<string, TimeSpan> _expirationConfiguration;

        public MemoryCacheStore(IMemoryCache memoryCache, Dictionary<string, TimeSpan> expirationConfiguration)
        {
            _memoryCache = memoryCache;
            this._expirationConfiguration = expirationConfiguration;
        }

        /// <summary>
        /// Add item to cache Store. If use timespan, servier if set expire time for this cache
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="key"></param>
        /// <param name="item">Collection, Object, ...</param>
        public void Add<TItem>(string key, TItem item)
        {
            //var cachedObjectName = item.GetType().Name;
            //var timespan = _expirationConfiguration[cachedObjectName];
            this._memoryCache.Set(key, item);
            //this._memoryCache.Set(key.CacheKey, item, timespan);
        }

        /// <summary>
        /// Get value in cache store by key
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public TItem Get<TItem>(string key) where TItem : class
        {
            if (this._memoryCache.TryGetValue(key, out TItem value))
            {
                return value;
            }
            return null;
        }

        /// <summary>
        /// remove cache by key
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="key"></param>
        public void Remove<TItem>(string key)
        {
            this._memoryCache.Remove(key);
        }
    }
}
