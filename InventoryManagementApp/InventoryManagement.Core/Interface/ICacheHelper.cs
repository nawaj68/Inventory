using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManagement.Core.Interface
{
    public interface ICacheHelper
    {
        Task<T> GetAsync<T>(string cacheKey);
        Task AddAsync(string cacheKey, object data, uint? durationInSeconds = null);
        Task UpdateAsync(string cacheKey, object data, uint? durationInSeconds = null);
        Task RemoveAsync(string cacheKey);
        Task RemoveByPatternAsync(string cacheKeyStartsWith);
        Task DeleteHashByPatternAsync(string pattern);
        Task SetRootHashSetAsync<T>(T data, string hashKey) where T : class;
        Task SetChildHashSetAsync<T>(T data, string hashKey);
        Task SetHashSetAsync(string hashKey, string key, string value);
        Task<object> GetHashSetAsync(string hashKey, string key);
        Task<T> GetChildHashSetAsync<T>(string hashKey);
        Task<bool> HashExistsAsync(string hashKey, string key);
        Task<T> GetHashSetAsync<T>(string hashKey) where T : class;
        Task<IEnumerable<string>> GetAllHashKeysAsync(string pattern);
        Task DeleteHashAsync(string hashKey);
    }
}
