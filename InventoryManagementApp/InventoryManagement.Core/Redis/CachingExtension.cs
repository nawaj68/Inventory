using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace InventoryManagement.Core.Redis
{
    public static class CachingExtension
    {
        public static void AddCaching(this IServiceCollection services, IConfiguration configuration)
        {
            //in-memory storage
            //services.AddDistributedMemoryCache();

            //sql server storage
            //services.AddDistributedSqlServerCache(options =>
            //{
            //    options.ConnectionString =
            //        configuration.GetConnectionString("PolicyAdmin");
            //    options.SchemaName = "dbo";
            //    options.TableName = "Cache";
            //});

            // redis storage
            var redisConfig = configuration.GetSection("AppSettings:Redis").Get<RedisConfiguration>();
            services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>(redisConfig);
        }
    }
}
