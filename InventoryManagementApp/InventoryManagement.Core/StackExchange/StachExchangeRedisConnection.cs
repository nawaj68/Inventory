using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;
using System;

namespace InventoryManagement.Core.StackExchange
{
    //public class StachExchangeRedisConnection
    //{
    //    private readonly static string RedisConnectionString = ConfigurationManager.AppSettings["RedisConnectionString"];
    //    private static Lazy<ConnectionMultiplexer> LazyConnection;
    //    //private static ConfigurationOptions configOptions = new ConfigurationOptions
    //    //{
    //    //    EndPoints =
    //    //    {
    //    //        { "redis0", 6379 },
    //    //        { "redis1", 6380 }
    //    //    },
    //    //    CommandMap = CommandMap.Create(new HashSet<string>
    //    //    {   // EXCLUDE a few commands
    //    //        "INFO", "CONFIG", "CLUSTER",
    //    //        "PING", "ECHO", "CLIENT"
    //    //    }, available: false),
    //    //    KeepAlive = 180,
    //    //    DefaultVersion = new Version(2, 8, 8),
    //    //    Password = "changeme"
    //    //};

    //    //RedisConnectionString =   "redis0:6380,redis1:6380,allowAdmin=true"
    //    //"127.0.0.1:6379,ssl=False,allowAdmin=True,abortConnect=False,defaultDatabase=0,connectTimeout=500,connectRetry=3"
    //    //AzureRedisConeectionString = "contoso5.redis.cache.windows.net,ssl=true,password=..."
    //    private static ConfigurationOptions configOptions = ConfigurationOptions.Parse(RedisConnectionString);

    //    static StachExchangeRedisConnection()
    //    {
    //        LazyConnection = new Lazy<ConnectionMultiplexer>(() =>
    //        {
    //            return ConnectionMultiplexer.Connect(configOptions);
    //        });
    //    }

    //    public ConnectionMultiplexer Connection { get; set; } = LazyConnection.Value;

    //    public IDatabase Database
    //    {
    //        get
    //        {
    //            if (Connection == null)
    //            {
    //                InitializeConnection();
    //            }
    //            return Connection != null ? Connection.GetDatabase() : null;
    //            //return Connection.GetDatabase(1);
    //        }
    //    }

    //    private void InitializeConnection()
    //    {
    //        try
    //        {
    //            Connection = ConnectionMultiplexer.Connect(RedisConnectionString);
    //        }
    //        catch (RedisConnectionException errorConnectionException)
    //        {
    //            Debug.WriteLine("Error connecting the redis cache : " + errorConnectionException.Message, errorConnectionException);
    //        }
    //    }
    //}



    public class RedisCachingProvider : ICachingProvider
    {
        protected IDatabase Redis { get; set; }

        public RedisCachingProvider() : this("localhost")
        {
        }

        public RedisCachingProvider(string host, bool ssl = true, int? defaultDatabase = null)
        {
            if (String.IsNullOrEmpty(host)) throw new ArgumentNullException("host");

            var configOptions = new ConfigurationOptions
            {
                EndPoints = { { host } },
                Ssl = ssl,
                DefaultDatabase = defaultDatabase
            };

            Initialize(configOptions);
        }

        public RedisCachingProvider(string host, int port, bool ssl = true, int? defaultDatabase = null)
        {
            if (String.IsNullOrEmpty(host)) throw new ArgumentNullException("host");

            var configOptions = new ConfigurationOptions
            {
                EndPoints = { { host, port } },
                Ssl = ssl,
                DefaultDatabase = defaultDatabase
            };

            Initialize(configOptions);
        }

        public RedisCachingProvider(string host, int port, string password, bool ssl = true, int? defaultDatabase = null)
        {
            if (String.IsNullOrEmpty(host)) throw new ArgumentNullException("host");

            var configOptions = new ConfigurationOptions
            {
                EndPoints = { { host, port } },
                Ssl = ssl,
                Password = password,
                DefaultDatabase = defaultDatabase
            };

            Initialize(configOptions);
        }

        public RedisCachingProvider(ConfigurationOptions configOptions)
        {
            if (configOptions == null) throw new ArgumentNullException("configOptions");

            Initialize(configOptions);
        }

        private void Initialize(ConfigurationOptions configOptions)
        {
            if (RedisConnector.Connection == null)
            {
                configOptions.AbortOnConnectFail = false;
                RedisConnector.Connection = ConnectionMultiplexer.Connect(configOptions);
            }

            Redis = RedisConnector.Connection.GetDatabase();
        }

        /// <summary>
        /// Insert value into the cache using
        /// appropriate name/value pairs
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="value">Item to be cached</param>
        /// <param name="key">Name of item</param>
        /// <param name="priority"></param>
        /// <param name="timeoutInSeconds">Seconds to cache</param>
        public void Set<T>(string key, T value, CacheItemPriority priority = CacheItemPriority.Normal, int? timeoutInSeconds = null)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException("key");

            TimeSpan? expiry = null;
            if (timeoutInSeconds.HasValue)
            {
                expiry = new TimeSpan(0, 0, 0, timeoutInSeconds.Value);
            }

            Redis.Set(key, value, expiry);
        }

        /// <summary>
        /// Remove item from cache
        /// </summary>
        /// <param name="key">Name of cached item</param>
        public void Clear(string key)
        {
            if (String.IsNullOrEmpty(key)) throw new ArgumentNullException("key");

            Redis.KeyDelete(key);
        }

        /// <summary>
        /// Check for item in cache
        /// </summary>
        /// <param name="key">Name of cached item</param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            if (String.IsNullOrEmpty(key)) throw new ArgumentNullException("key");

            return Redis.KeyExists(key);
        }

        /// <summary>
        /// Retrieve cached item
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="key">Name of cached item</param>
        /// <param name="value">Cached value. Default(T) if
        /// item doesn't exist.</param>
        /// <returns>Cached item as type</returns>
        public bool Get<T>(string key, out T value)
        {
            if (String.IsNullOrEmpty(key)) throw new ArgumentNullException("key");

            try
            {
                value = Redis.Get<T>(key);

                if (Equals(value, default(T)))
                {
                    value = default(T);
                    return false;
                }
            }
            catch
            {
                value = default(T);
                return false;
            }

            return true;
        }

        public int Increment(string key, int defaultValue, int value, CacheItemPriority priority = CacheItemPriority.Normal)
        {
            if (String.IsNullOrEmpty(key)) throw new ArgumentNullException("key");

            // no need to use a lock since the redis increment method is atomic already
            return Convert.ToInt32(Redis.StringIncrement(key, value));
        }

        public void Dispose()
        {

        }
    }
}
