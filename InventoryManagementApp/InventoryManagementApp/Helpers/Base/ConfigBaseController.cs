using InventoryManagement.Core;
using InventoryManagement.Core.Interface;
using InventoryManagement.Service.Services.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace InventoryManagement.Helpers.Base
{
    public abstract class ConfigBaseController<T> : UserInfoBase where T : class
    {
        protected readonly IBaseService<T> _service;
        private readonly ICacheHelper _redisCacheClient;
        private readonly CacheConfig _appSettings;
        uint _cacheDuration;

        public ConfigBaseController(IBaseService<T> service,
            ICacheHelper redisCacheClient,
            IOptions<AppSettings> appSettings)
        {
            _service = service;
            _redisCacheClient = redisCacheClient;
            _appSettings = appSettings.Value.CacheConfig;
            _cacheDuration = _appSettings.BaseControllerCacheDuration;
        }

        [HttpGet("get/{id:long}")]
        public virtual async Task<IActionResult> GetByIdAsync(long id)
        {
            var cacheKey = $"{typeof(T).Name.ToLower()}-{id}";
            var configFromCache = await _redisCacheClient.GetAsync<T>(cacheKey);
            if (configFromCache != null)
            {
                Response.Headers.Add("X-DataSource", $"From-Cache");
                return Ok(configFromCache);
            }
            var res = await _service.FirstOrDefaultAsync(id);
            if (res != null)
            {
                await _redisCacheClient.AddAsync(cacheKey, res, _cacheDuration);
            }
            return Ok(res);
        }

        [HttpGet("gets")]
        public virtual async Task<IActionResult> GetsAsync()
        {
            var cacheKey = $"{typeof(T).Name.ToLower()}-all";
            var configFromCache = await _redisCacheClient.GetAsync<List<T>>(cacheKey);
            if (configFromCache != null)
            {
                Response.Headers.Add("X-DataSource", $"From-Cache");
                return Ok(configFromCache);
            }
            var res = await _service.GetAllAsync();
            if (res.Count > 0)
            {
                await _redisCacheClient.AddAsync(cacheKey, res, _cacheDuration);
            }
            return Ok(res);
        }

        [HttpPost("add")]
        public virtual async Task<IActionResult> AddAsync(T entity)
        {
            var cacheKey = $"{typeof(T).Name.ToLower()}";
            await _redisCacheClient.RemoveByPatternAsync(cacheKey);
            var res = await _service.InsertAsync(entity);
            Type t = res.GetType();
            PropertyInfo prop = t.GetProperty("Id");
            object id = prop.GetValue(res);
            cacheKey = $"{cacheKey}-{id}";

            await _redisCacheClient.AddAsync(cacheKey, res, _cacheDuration);

            return Created("", res);
        }

        [HttpPut("edit/{id:long}")]
        public virtual async Task<IActionResult> EditAsync(long id, T entity)
        {
            var cacheKey = $"{typeof(T).Name.ToLower()}";
            await _redisCacheClient.RemoveByPatternAsync(cacheKey);
            var dbRes = await _service.FirstOrDefaultAsync(id);
            //dbRes.CopyPropertiesFrom(entity);
            var res = await _service.UpdateAsync(id, dbRes);
            //var res = await _service.UpdateAsync(id, entity);
            cacheKey = $"{cacheKey}-{id}";
            await _redisCacheClient.AddAsync(cacheKey, res, _cacheDuration);

            return Ok(res);
        }

        [HttpDelete("delete/{id:long}")]
        public virtual async Task<IActionResult> DeleteAsync(long id)
        {
            await _service.DeleteAsync(id);
            await _redisCacheClient.RemoveByPatternAsync($"{typeof(T).Name.ToLower()}");

            return NoContent();
        }

        [HttpPost("delete")]
        public virtual async Task<IActionResult> DeleteAsync(T entity)
        {
            Type type = entity.GetType();
            long Id = (long)type.GetProperty("Id").GetValue(entity);

            await _service.DeleteAsync(Id);
            await _redisCacheClient.RemoveByPatternAsync($"{typeof(T).Name.ToLower()}");

            return NoContent();
        }
    }

}
