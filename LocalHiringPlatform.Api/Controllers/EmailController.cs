using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LocalHiringPlatform.Api.Controllers
{
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
                _emailService = emailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail(
                EmailRequestModel request)
        {
            await _emailService.SendEmailAsync(request);

            return Ok();
        }
    }
}
