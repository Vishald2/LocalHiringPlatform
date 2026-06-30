using LocalHiringPlatform.Api.DTOs;
using LocalHiringPlatform.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

namespace LocalHiringPlatform.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class MobileVerificationController : ControllerBase
    {
        private readonly ISmsService _smsService;

        public MobileVerificationController(ISmsService smsService)
        {
            _smsService = smsService;
        }

        [HttpPost("sendotp")]
        public async Task<IActionResult> SendOtp([FromBody] string mobileNumber)
        {
            await _smsService.SendOtpAsync(mobileNumber);

            return Ok();
        }
    }
}