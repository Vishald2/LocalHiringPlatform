using LocalHiringPlatform.Api.Extensions;
using LocalHiringPlatform.Domain.Interfaces.AI;
using LocalHiringPlatform.Domain.Models.AI;
using LocalHiringPlatform.Infrastructure.Services.AI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocalHiringPlatform.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class AIChatController : ControllerBase
    {
        private readonly IAIChatService _aiChatService;
        private readonly IAIStreamingChatService _aiStreamingChatService;

        public AIChatController(
            IAIChatService aiChatService,
            IAIStreamingChatService aiStreamingChatService)
        {
            _aiChatService = aiChatService;
            _aiStreamingChatService = aiStreamingChatService;
        }

        [HttpPost]
        public async Task<IActionResult> Chat(
            [FromBody] AIChatRequestModel request)
        {
            var response =
                await _aiChatService.SendMessageAsync(
                    request);

            return Ok(response);
        }

        [HttpPost("stream")]
       // [Authorize]
        public async Task<IActionResult> Stream([FromBody] AIStreamingRequest model, CancellationToken cancellationToken)
        {
            var userId = User.GetUserId().ToString();   // however you're currently extracting it

            await _aiStreamingChatService.StreamAsync(
                userId,
                model,
                cancellationToken);

            return Accepted();
        }
    }
}