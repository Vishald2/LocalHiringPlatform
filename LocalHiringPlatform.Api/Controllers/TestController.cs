using LocalHiringPlatform.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocalHiringPlatform.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{

    private readonly IRedisCacheService _redisCacheService;

    public TestController(
        IRedisCacheService redisCacheService)
    {
        _redisCacheService = redisCacheService;
    }

    [Authorize]
    [HttpGet("protected")]
    public IActionResult Protected()
    {
        return Ok(new
        {
            Message = "You are authenticated"
        });
    }

    [HttpGet("redis-test")]
    public async Task<IActionResult> RedisTest()
    {
        await _redisCacheService.SetAsync(
            "TestKey",
            "Hello Redis",
            TimeSpan.FromMinutes(5));

        var value =
            await _redisCacheService.GetAsync<string>(
                "TestKey");

        return Ok(value);
    }

    [HttpGet("redis-ping")]
    public async Task<IActionResult> RedisPing()
    {
        try
        {
            await _redisCacheService.SetAsync(
                "Ping",
                "Pong");

            var value =
                await _redisCacheService.GetAsync<string>(
                    "Ping");

            return Ok(new
            {
                Success = true,
                Value = value
            });
        }
        catch (Exception ex)
        {
            return Ok(new
            {
                Success = false,
                Message = ex.Message,
                InnerException = ex.InnerException?.Message
            });
        }
    }
}