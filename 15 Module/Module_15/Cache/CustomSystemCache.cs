using Cache.Interface;
using System;
using System.Linq;
using System.Runtime.Caching;

namespace Cache
{
    public class CustomSystemCache<T> : ICache<T>
    {
        private readonly ObjectCache _cache;

        public CustomSystemCache()
        {
            _cache = MemoryCache.Default;
        }

        public T Get(string key)
        {
            var value = _cache.Get(key);
            if(value == null)
                return default(T);
            return (T)value;
        }

        public void Set(string key, T value, DateTimeOffset expirationDate)
        {                 
            _cache.Set(key, value, expirationDate);
        }

        public void Set(string key, T value, CacheItemPolicy policy)
        {
            _cache.Set(key, value, policy);
        }
    }
}
