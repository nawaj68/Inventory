using Microsoft.Extensions.Options;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagement.Core.Extensions;
using InventoryManagement.Core.Interface;
using InventoryManagement.Core.Helpers;
using InventoryManagement.Core;

namespace InventoryManagement.Core.Helpers
{
    public class CacheHelper : ICacheHelper
    {
        private readonly IRedisCacheClient _redisCacheClient;
        private readonly CacheConfig _cacheConfig;

        public CacheHelper(IRedisCacheClient redisCacheClient, IOptions<AppSettings> appSettings)
        {
            _redisCacheClient = redisCacheClient;
            _cacheConfig = appSettings.Value.CacheConfig;
        }

        public async Task<T> GetAsync<T>(string cacheKey)
        => await _redisCacheClient.Db0.GetAsync<T>(cacheKey);

        public async Task AddAsync(string cacheKey, object data, uint? durationInSeconds = null)
        => await _redisCacheClient.Db0.ReplaceAsync(cacheKey, data, DateTimeOffset.UtcNow.AddSeconds(durationInSeconds ?? _cacheConfig.BaseControllerCacheDuration));

        public async Task UpdateAsync(string cacheKey, object data, uint? durationInSeconds = null)
        {
            await RemoveAsync(cacheKey);
            await AddAsync(cacheKey, data, durationInSeconds ?? _cacheConfig.BaseControllerCacheDuration);
        }

        public async Task RemoveAsync(string cacheKey)
        => await _redisCacheClient.Db0.RemoveAsync(cacheKey);

        public async Task RemoveByPatternAsync(string cacheKeyStartsWith)
        {
            var keys = await _redisCacheClient.Db0.SearchKeysAsync($"{cacheKeyStartsWith}*");
            await _redisCacheClient.Db0.RemoveAllAsync(keys);
        }

        public async Task DeleteHashAsync(string hashKey)
        {
            var existingKeys = await _redisCacheClient.Db0.HashKeysAsync(hashKey);
            await _redisCacheClient.Db0.HashDeleteAsync(hashKey, existingKeys);
        }

        public async Task DeleteHashByPatternAsync(string pattern)
        {
            var existingKey = await _redisCacheClient.Db0.SearchKeysAsync($"{pattern}*");
            foreach (var hashKey in existingKey)
                await this.DeleteHashAsync(hashKey);
        }

        public async Task<IEnumerable<string>> GetAllHashKeysAsync(string pattern)
        {
            var existingKey = await _redisCacheClient.Db0.SearchKeysAsync($"{pattern}*");
            return existingKey;
        }

        public async Task SetRootHashSetAsync<T>(T data, string hashKey) where T : class
        {
            await this.DeleteHashAsync(hashKey);
            var rootObjectDictionary = data.ToDictionary();
            await _redisCacheClient.Db0.HashSetAsync(hashKey, rootObjectDictionary);
        }

        public async Task SetChildHashSetAsync<T>(T data, string hashKey)
        {
            await _redisCacheClient.Db0.HashSetAsync(hashKey, typeof(T).Name, data);
        }

        public async Task SetHashSetAsync(string hashKey, string key, string value)
        {
            await _redisCacheClient.Db0.HashSetAsync(hashKey, key, value);
        }

        public async Task<object> GetHashSetAsync(string hashKey, string key)
        {
            return await _redisCacheClient.Db0.HashGetAsync<object>(hashKey, key);
        }

        public async Task<T> GetChildHashSetAsync<T>(string hashKey)
        {
            var childObject = await _redisCacheClient.Db0.HashGetAsync<T>(hashKey, typeof(T).Name);
            return childObject;
        }

        public async Task<bool> HashExistsAsync(string hashKey, string key)
        => await _redisCacheClient.Db0.HashExistsAsync(hashKey, key);


        public async Task<T> GetHashSetAsync<T>(string hashKey) where T : class
        {
            var allKeys = await _redisCacheClient.Db0.HashKeysAsync(hashKey);
            var rootDataWithAllChild = await _redisCacheClient.Db0.HashGetAsync<object>(hashKey, allKeys.ToList());
            var allData = rootDataWithAllChild.ToTypeOf<T>();
            return allData;
        }
    }
}