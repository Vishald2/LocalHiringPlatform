using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Interfaces.AI;
using LocalHiringPlatform.Domain.Models;
using LocalHiringPlatform.ServiceBus.Interfaces;
using LocalHiringPlatform.ServiceBus.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocalHiringPlatform.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ATestController : ControllerBase
{
    private readonly ILogger _logger;
    private IPromptService _promptService;
    private readonly IServiceBusPublisher _serviceBusPublisher;

    public ATestController(
        ILogger<ATestController> logger,
        IPromptService promptService,
        IServiceBusPublisher serviceBusPublisher
    )
    {
        _logger = logger;
        _promptService = promptService;
        _serviceBusPublisher = serviceBusPublisher;  
    }

    [HttpPost("publish")]
    public async Task<IActionResult> PublishAsync()
    {
        var message = new EmailRequestModel
        {
            To = "vishald3511@gmail.com",
            Subject = "Azure Service Bus Test",
            HtmlBody = "<h2>Hello from LocalHire</h2>"
        };

       await _serviceBusPublisher.PublishAsync(message);



        return Ok();
    }

    [HttpGet("LogMessage")]
    public async Task<IActionResult> LogMessage()
    {
        _logger.LogInformation("Message from TestController at : " +  DateTime.Now.ToString());

        return Ok("Message from TestController at : " + DateTime.Now.ToString());
    }

    [HttpGet("LogException")]
    public async Task<IActionResult> LogException()
    {
        _logger.LogError("LogExceptio- Error log at: "  +DateTime.Now.ToString());

        throw new Exception("Exception thrown by TestController(LogException) at: " + DateTime.Now.ToString());
    }
}