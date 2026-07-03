using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Interfaces.AI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocalHiringPlatform.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{

    private readonly IRedisCacheService _redisCacheService;
    private readonly ILogger _logger;
    private IPromptService _promptService;

    public TestController(
        IRedisCacheService redisCacheService,
        ILogger<TestController> logger,
        IPromptService promptService)
    {
        _redisCacheService = redisCacheService;
        _logger = logger;
        _promptService = promptService;
    }


    [HttpGet("prompt")]
    public async Task<IActionResult> TestPrompt()
    {
        var prompt =
            await _promptService.GetPromptAsync(
                "JobSearchIntentPrompt.txt");

        return Ok(prompt);
    }

    [HttpGet]
    public IActionResult Test()
    {
        throw new Exception("This is a test exception for logging purposes.");
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