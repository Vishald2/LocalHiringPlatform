using LocalHiringPlatform.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using System;
using System.Text.Json;

namespace LocalHiringPlatform.Infrastructure.Services
{
    public class RedisCacheService
        : IRedisCacheService
    {
        private readonly IConnectionMultiplexer
            _connectionMultiplexer;

        public RedisCacheService(
            IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer =
                connectionMultiplexer;
        }

        public async Task<T?> GetAsync<T>(
            string key)
        {
            var database = _connectionMultiplexer.GetDatabase();

            var value =
                await database.StringGetAsync(key);

            if (!value.HasValue)
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(
                value!);
        }

        public async Task SetAsync<T>(
            string key,
            T value,
            TimeSpan? expiry = null)
        {
            var json = JsonSerializer.Serialize(value);

            var database = _connectionMultiplexer.GetDatabase();

            if (expiry != null)
            {
                await database.StringSetAsync(
                    key,
                    json,
                    (Expiration)expiry);
            }
            else
            {
                await database.StringSetAsync(
                    key,
                    json);
            }
        }

        public async Task RemoveAsync(
            string key)
        {
            var database = _connectionMultiplexer.GetDatabase();

            await database.KeyDeleteAsync(key);
        }
    }
}