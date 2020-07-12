using Cache;
using NorthwindLibrary;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using Cache.Interface;

namespace ConsoleDemonstrationCache
{
    public class EntitiesManager<T> where T : class
    {
        private readonly ICache<IEnumerable<T>> _cache;

        public EntitiesManager(ICache<IEnumerable<T>> cache)
        {
            _cache = cache;
        }

        public IEnumerable<T> GetEntities()
        {
            Console.WriteLine("Get Entities");
            var user = Thread.CurrentPrincipal.Identity.Name;
            var entities = _cache.Get(user + "_" + typeof(T).Name);

            if (entities == null)
            {
                Console.WriteLine("From no cache storage");
                using (var dbContext = new Northwind())
                {
                    dbContext.Configuration.LazyLoadingEnabled = false;
                    dbContext.Configuration.ProxyCreationEnabled = false;
                    entities = dbContext.Set<T>().ToList();
                }

                _cache.Set(user + "_" + typeof(T).Name, entities, DateTimeOffset.Now.AddDays(1));
            }
            return entities;
        }
    }
}