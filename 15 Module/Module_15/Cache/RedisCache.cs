using Cache.Interface;
using StackExchange.Redis;
using System;
using System.IO;
using System.Runtime.Serialization;

namespace Cache
{
    public class RedisCache<T> : ICache<T>
    {
        private const string hostName = "localhost";
        private readonly ConnectionMultiplexer _redisConnection;      

        private readonly DataContractSerializer _serializer = new DataContractSerializer(typeof(T));

        public RedisCache()
        {           
            var options = new ConfigurationOptions()
            {
                AbortOnConnectFail = false,
                EndPoints = { hostName }
            };
            _redisConnection = ConnectionMultiplexer.Connect(options);
        }

        public T Get(string key)
        {
            var cache = _redisConnection.GetDatabase();
            byte[] str = cache.StringGet(key);
            if (str == null)
            {
                return default(T);
            }

            return (T)_serializer.ReadObject(new MemoryStream(str));
        }

        public void Set(string key, T value, DateTimeOffset expirationDate)
        {
            var cache = _redisConnection.GetDatabase();         

            if (value == null)
            {
                cache.StringSet(key, RedisValue.Null);
            }
            else
            {
                var stream = new MemoryStream();
                _serializer.WriteObject(stream, value);
                cache.StringSet(key, stream.ToArray(), expirationDate - DateTimeOffset.Now);
            }
        }
    }
}