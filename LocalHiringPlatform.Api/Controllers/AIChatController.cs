using LocalHiringPlatform.Domain.Interfaces.AI;
using LocalHiringPlatform.Domain.Interfaces.AI.LocalHiringPlatform.Domain.Interfaces.AI;
using LocalHiringPlatform.Domain.Models.AI;
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

        public AIChatController(
            IAIChatService aiChatService)
        {
            _aiChatService = aiChatService;
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
    }
}