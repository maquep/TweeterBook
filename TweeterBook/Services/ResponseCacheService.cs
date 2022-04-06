using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace TweeterBook.Services
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDistributedCache _distributeCache;

        public ResponseCacheService(IDistributedCache distributedCache)
        {
            _distributeCache = distributedCache;
        }

        public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive)
        {
            if (response == null)
            {
                return;
            }

            var serializedResponse = JsonConvert.SerializeObject(response);

            await _distributeCache.SetStringAsync(cacheKey, serializedResponse, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeToLive
            });
        }

        public async Task<string> GetCachedResponseAsync(string cacheKey)
        {
            var cachedResponse = await _distributeCache.GetStringAsync(cacheKey);
            return string.IsNullOrEmpty(cachedResponse) ? null : cachedResponse;
        }
    }
}
