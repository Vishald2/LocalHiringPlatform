using System;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Interfaces
{
    public interface IRedisCacheService
    {
        Task<T?> GetAsync<T>(string key);

        Task SetAsync<T>(
            string key,
            T value,
            TimeSpan? expiry = null);

        Task RemoveAsync(string key);
    }
}